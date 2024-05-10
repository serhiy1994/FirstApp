import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ListModel } from './models/list.model';
import { CardModel } from './models/card.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5000/api'; // Your API base URL

  constructor(private http: HttpClient) { }

  // Activity logging
  logActivity(activityType: number, oldValue?: string, newValue?: string): Observable<any> {
    const activity = { type: activityType, oldValue: oldValue, newValue: newValue, date: new Date() };
    return this.http.post(`${this.apiUrl}/activities`, activity);
  }

  // List Operations
  getLists(): Observable<ListModel[]> {
    return this.http.get<ListModel[]>(`${this.apiUrl}/lists`);
  }

  addList(list: ListModel): Observable<ListModel> {
    return this.http.post<ListModel>(`${this.apiUrl}/lists`, list).pipe(
      tap((newList) => this.logActivity(1, undefined, newList.listName).subscribe())
    );
  }

  updateList(list: ListModel, oldName: string): Observable<ListModel> {
    return this.http.put<ListModel>(`${this.apiUrl}/lists/${list.id}`, list).pipe(
      tap((updatedList) => this.logActivity(3, oldName, updatedList.listName).subscribe())
    );
  }

  deleteList(id: number, oldName: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/lists/${id}`).pipe(
      tap(() => this.logActivity(2, oldName, undefined).subscribe())
    );
  }

  // Card Operations
  getCards(): Observable<CardModel[]> {
    return this.http.get<CardModel[]>(`${this.apiUrl}/cards`);
  }

  addCard(card: CardModel): Observable<CardModel> {
    return this.http.post<CardModel>(`${this.apiUrl}/cards`, card).pipe(
      tap((newCard) => this.logActivity(1, undefined, newCard.cardName).subscribe())
    );
  }

  updateCard(card: CardModel, oldName: string): Observable<CardModel> {
    return this.http.put<CardModel>(`${this.apiUrl}/cards/${card.id}`, card).pipe(
      tap((updatedCard) => this.logActivity(3, oldName, updatedCard.cardName).subscribe())
    );
  }

  deleteCard(id: number, oldName: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/cards/${id}`).pipe(
      tap(() => this.logActivity(2, oldName, undefined).subscribe())
    );
  }
}
