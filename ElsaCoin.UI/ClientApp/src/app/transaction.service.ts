import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

export interface Transaction {
    id?: number,
    fromAddress: string,
    toAddress: string,
    amount: number,
    isProcessed?: boolean,
}

@Injectable({
    providedIn: 'root'
})
export class TransactionService {

    constructor(private httpClient: HttpClient) {}

    addTransaction(transaction: Transaction): Observable<Transaction> {
        return this.httpClient.post<Transaction>(environment.apiUrl + '/transaction/add', transaction);
    }

    getPending(): Observable<Transaction> {
        return this.httpClient.get<Transaction>(environment.apiUrl + '/transaction/next-pending');
    }
}