import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Transaction } from "./transaction.service";

export interface Block {
    id?: number,
    timeStamp: string,
    transactions: Transaction[],
    previousHash: string,
    hash: string,
    nonce: number   
}

@Injectable({
    providedIn: 'root'
})
export class BlockchainService {

    address: string;
    
    constructor(private httpClient: HttpClient) {}

    getMiningReward(): Observable<number> {
        return this.httpClient.get<number>(environment.apiUrl + '/blockchain/reward');
    }

    getBalance(address?: string): Observable<number> {
        return this.httpClient.get<number>(environment.apiUrl + '/blockchain/balance/' + (address || this.address));
    }

    last50(): Observable<Block[]> {
        return this.httpClient.get<Block[]>(environment.apiUrl + '/blockchain/last50');
    }

    current(): Observable<Block> {
        return this.httpClient.get<Block>(environment.apiUrl + '/blockchain/current');
    }

    getDifficulty(): Observable<number> {
        return this.httpClient.get<number>(environment.apiUrl + '/blockchain/difficulty');
    }

    addBlock(block: Block): Observable<number> {
        return this.httpClient.post<number>(environment.apiUrl + '/blockchain/add', block);
    }
}