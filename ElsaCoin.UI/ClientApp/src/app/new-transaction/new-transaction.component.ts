import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { BlockchainService } from '../blockchain.service';
import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.css']
})
export class NewTransactionComponent {

  toAddress: string = '';
  amount: number = 0;

  constructor(
    private blockchainService: BlockchainService,
    private transactionService: TransactionService,
    private router: Router,
  ) { }

  get address() {
    return this.blockchainService.address;
  }

  createTransaction() {
    this.transactionService.addTransaction(
      {
        fromAddress: this.address,
        toAddress: this.toAddress,
        amount: this.amount,
      }).pipe(first())
      .subscribe(() => {
        this.router.navigateByUrl('/');
    })
  }

}
