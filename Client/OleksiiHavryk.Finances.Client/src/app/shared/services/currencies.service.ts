import { Injectable } from '@angular/core';
import { Currency } from '../domain/currency';
import { Observable, tap, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CurrenciesService {
  private innerCurrencies: Currency[] = [];
  private get url(): string {
    return environment.server.ip + "/currency";
  } 

  public get isHaveAny(): boolean {
    return this.innerCurrencies.length > 0;
  }
  public get currencies(): Currency[] {
    return this.innerCurrencies;
  }

  constructor(private httpClient: HttpClient) { 
  }

  public add(curr: Currency): Observable<Currency[]>
  {
    return this.httpClient.post<Currency>(this.url, curr).pipe(
      tap((c) => {
        console.log(`Currency is created!`),
        console.log(c)
      }),
      switchMap(() => this.updateSet(true))
    );;
  }
  public remove(id: string): Observable<Currency[]>
  {
    return this.httpClient.delete<void>(this.url + `/id/${id}`).pipe(
      tap(() => console.log(`Currency with id '${id}' is deleted!`)),
      switchMap(() => this.updateSet(true))
    );
  }
  public updateSet(updated: boolean): Observable<Currency[]>
  {
    return this.httpClient.get<Currency[]>(this.url).pipe(
      tap((c) => {
        this.innerCurrencies = c ?? [];
        if (updated) { console.log("Currencies set is updated!"); }
        console.log(`Current currencies set - `);
        console.log(this.innerCurrencies);
      })
    );
  }
  public currencyById(id: string)
  {
    return this.innerCurrencies.filter(c => c.id === id)[0];
  }
}
