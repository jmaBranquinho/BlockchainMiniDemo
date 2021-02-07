import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import CryptoJS from 'crypto-js';
import { forkJoin } from 'rxjs';
import { first } from 'rxjs/operators';
import { ApplicationPaths } from 'src/api-authorization/api-authorization.constants';
import { AuthorizeService, IUser } from 'src/api-authorization/authorize.service';
import { Block, BlockchainService } from '../blockchain.service';
import { Transaction, TransactionService } from '../transaction.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  balance: number = 0;
  miningReward: number = 0;
  isLoading: boolean = true;
  isMining: boolean = false;
  isAuthenticated: boolean;
  user: IUser;
  blocks: Block[];

  constructor(
    private blockchainService: BlockchainService,
    private authorizeService: AuthorizeService,
    private transactionService: TransactionService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.authorizeService
      .isAuthenticated()
      .pipe(first())
      .subscribe(isAuthenticated => {
        this.isLoading = false;
        this.isAuthenticated = isAuthenticated;
        if (isAuthenticated) {
          this.authorizeService
            .getUser()
            .pipe(first())
            .subscribe(user => {
              this.user = user;
              this.blockchainService.address = this.user.name;
              forkJoin([
                this.blockchainService.getMiningReward(),
                this.blockchainService.getBalance(this.user.name),
                this.blockchainService.last50(),
              ])
                .pipe(first())
                .subscribe((res: any[]) => {
                  this.miningReward = res[0];
                  this.balance = res[1];
                  this.blocks = res[2];
                });
            });
        } else {
          setTimeout(() => {
            this.router.navigate(ApplicationPaths.LoginPathComponents);
          }, 2000);
        }
      });
  }

  startMining() {
    this.isMining = true;
    this.transactionService
      .getPending()
      .pipe(first())
      .subscribe((transaction: Transaction) => {
        if (transaction) {
          this.minePendingTransactions(transaction);
        } else {
          this.isMining = false;
        }
      });
  }

  private minePendingTransactions(transaction: Transaction): void {
    forkJoin([
      this.blockchainService.current(),
      this.blockchainService.getDifficulty()
    ]).pipe(first()).subscribe((res: any[]) => {
      const previousBlock = res[0];
      const difficulty = res[1];
      let block: Block = {
        timeStamp: Date.now().toString(),
        transactions: [...(previousBlock.transactions || []), transaction],
        previousHash: previousBlock.hash,
        nonce: 0,
        hash: ''
      };
      this.mineBlock(block, difficulty).then((block) => {
        this.blockchainService.addBlock(block).pipe(first()).subscribe((i: number) => {
          block.id = i;
          this.isMining = false;
          this.blocks.push(block);
        });
      });
    });
  }

  private async mineBlock(block: Block, difficulty: number): Promise<Block> {
    while (block.hash.substring(0, difficulty) !== Array(difficulty + 1).join("0")) {
      block.nonce++;
      block.hash = this.calculateHash(block);
      await this.delay(1);
    }

    return block;
  }

  private calculateHash(block: Block): string {
    return this.encodeSha256(block.previousHash + block.timeStamp + JSON.stringify(block.transactions) + block.nonce).toString();
}

  private encodeSha256(message: string): string {
    return CryptoJS.SHA256(message);
  }

  private delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
}
}
