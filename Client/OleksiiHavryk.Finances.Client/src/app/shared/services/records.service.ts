import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Record } from '../domain/record';
import { environment } from 'src/environments/environment';
import { Observable, map, switchMap, tap } from 'rxjs';
import { Entry } from '../domain/entry';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {
  private innerRecords: Record[] = [];
  
  private get recordUrl(): string {
    return environment.server.ip + "/record";
  } 
  private get entryUrl(): string {
    return environment.server.ip + "/entry";
  } 

  public get isHaveAny(): boolean {
    return this.innerRecords.length > 0;
  }
  public get records(): Record[] {
    return this.innerRecords;
  }

  constructor(private httpClient: HttpClient) { 
  }

  public add(rec: Record): Observable<Record[]>
  {
    return this.httpClient.post<Record>(this.recordUrl, rec).pipe(
      tap((r) => {
        console.log(`Record is created!`),
        console.log(r)
      }),
      switchMap(() => this.updateSet(true))
    );
  }
  public remove(id: string): Observable<Record[]>
  {
    return this.httpClient.delete<void>(this.recordUrl + `/id/${id}`).pipe(
      tap(() => console.log(`Record with id '${id}' is deleted!`)),
      switchMap(() => this.updateSet(true))
    );
  }
  public addEntry(entry: Entry): Observable<Record[]>
  {
    return this.httpClient.post<Entry>(this.entryUrl, entry).pipe(
      tap((e) => {
        console.log(`Entry is created!`),
        console.log(e)
      }),
      switchMap(() => this.updateSet(true))
    ); 
  }
  public removeEntry(entryId: string): Observable<Record[]>
  {
    return this.httpClient.delete<void>(this.entryUrl + `/id/${entryId}`).pipe(
      tap(() => console.log(`Entry with id '${entryId}' is deleted!`)),
      switchMap(() => this.updateSet(true))
    );
  }
  public updateSet(updated: boolean): Observable<Record[]>
  {
    return this.httpClient.get<Record[]>(this.recordUrl).pipe(
      tap((r) => {
        this.innerRecords = r ?? [];
        if (updated) { console.log("Records set is updated!"); }
        console.log(`Current records set - `);
        console.log(this.innerRecords);
      })
    );
  }
}
