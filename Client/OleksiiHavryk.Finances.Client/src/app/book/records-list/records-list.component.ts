import { Component } from '@angular/core';
import { Entry } from 'src/app/shared/domain/entry';
import { Record } from 'src/app/shared/domain/record';
import { ModalService } from 'src/app/shared/modal/modal.service';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';
import { RecordsService } from 'src/app/shared/services/records.service';

@Component({
    selector: 'app-records-list',
    templateUrl: './records-list.component.html',
    styleUrls: ['./records-list.component.css'],
    standalone: false
})
export class RecordsListComponent {
  public idsOfOpenedRecords: string[] = [];

  public deleteRecordModalId: string = "DeleteRecordModalId";
  public deleteEntryModalId: string = "DeleteEntryModalId";

  public deletingRecord: Record | undefined;
  public deletingEntry: Entry | undefined;

  constructor(
    public recordsService: RecordsService,
    public currenciesService: CurrenciesService,
    public modal: ModalService) { 
    this.currenciesService.updateSet(false).subscribe(()=>{
      this.recordsService.updateSet(false).subscribe(()=>{});
    });
  }

  public addToOpened(id: string) {
    this.idsOfOpenedRecords.push(id);
  }
  public removeFromOpened(id: string) {
    this.idsOfOpenedRecords = this.idsOfOpenedRecords.filter(v => v !== id);
  }
  public isOpened(id: string): boolean {
    return this.idsOfOpenedRecords.find(v => v === id) !== undefined;
  }

  public removeEntry(entry: Entry) {
    this.deletingEntry = entry;
    this.modal.toggleModal(this.deleteEntryModalId);
  }
  public removeRecord(record: Record) {
    this.deletingRecord = record;
    this.modal.toggleModal(this.deleteRecordModalId);
  }

  public createRecordRemoveAction(): () => void {
    return () => {
      if (this.deletingRecord !== undefined) {
        this.recordsService.remove(this.deletingRecord.id).subscribe();
      }
    }
  }
  public createEntryRemoveAction(): () => void {
    return () => {
      if (this.deletingEntry !== undefined) {
        this.recordsService.removeEntry(this.deletingEntry.id).subscribe();
      }
    }
  }
}
