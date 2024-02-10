import { TestBed } from '@angular/core/testing';

import { RecordHelperService } from './record-helper.service';

describe('RecordHelperService', () => {
  let service: RecordHelperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecordHelperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
