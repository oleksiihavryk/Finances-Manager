import { Injectable } from '@angular/core';
import { Entry } from '../domain/entry';
import { Record } from '../domain/record';

@Injectable({
  providedIn: 'root'
})
export class RecordHelperService {
  private hiddenEntriesId: string[] = [];

  public getAvailableDebit(record: Record): Entry[] {
    return this.getAvailableEntries(record).filter(r => r.type == 1);
  }
  public getAvailableCredit(record: Record): Entry[] {
    return this.getAvailableEntries(record).filter(r => r.type == 2);
  }
  public getAvailableEntries(record: Record): Entry[] {
    const entries = record.entries;
    return entries.filter(e => this.hiddenEntriesId.includes(e.id) === false);
  }
  public toggleEntryVisibility(entry: Entry) {
    if (this.hiddenEntriesId.includes(entry.id)) {
      this.showEntry(entry);
      return;
    }

    this.hideEntry(entry);
  }
  public hideEntry(entry: Entry): void {
    this.hiddenEntriesId.push(entry.id);
  }
  public showEntry(entry: Entry): void {
    this.hiddenEntriesId = this.hiddenEntriesId.filter(v => v !== entry.id);
  }
  public isHidden(entry: Entry): boolean {
    return this.hiddenEntriesId.includes(entry.id);
  }
}
