import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ScrapingService } from '../../services/scraping.service';
import { MatPaginator } from '@angular/material/paginator';
import { OLDData } from '../../interfaces/old-data.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { WBData } from '../../interfaces/wb-data.model';
import { OFACData } from '../../interfaces/ofac-data.model';
import { ErrorService } from 'src/app/shared/services/error.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrorCode } from 'src/app/shared/interfaces/error-code.enum';
import { UNAUTHORIZED } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-supplier-dialog',
  templateUrl: './supplier-dialog.component.html',
  styleUrls: ['./supplier-dialog.component.css']
})
export class SupplierDialogComponent {
  form: FormGroup;
  buttonEnabled: boolean = false;
  pageSize = 5;
  loading = false;

  oldDataColumns: string[] = ['entity', 'jurisdiction', 'linkedTo', 'dataFrom'];
  oldDataSource!: MatTableDataSource<OLDData>;
  @ViewChild('oldDataSort') oldDataSort!: MatSort;
  @ViewChild('oldDataPaginator') oldDataPaginator!: MatPaginator;

  wbDataColumns: string[] = ['firmName', 'address', 'country', 'fromDate', 'toDate', 'grounds'];
  wbDataSource!: MatTableDataSource<WBData>;
  @ViewChild('wbDataSort') wbDataSort!: MatSort;
  @ViewChild('wbDataPaginator') wbDataPaginator!: MatPaginator;

  ofacDataColumns: string[] = ['name', 'address', 'type', 'programs', 'list', 'score'];
  ofacDataSource!: MatTableDataSource<OFACData>;
  @ViewChild('ofacDataSort') ofacDataSort!: MatSort;
  @ViewChild('ofacDataPaginator') ofacDataPaginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<SupplierDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public supplierName: string,
    private scrapingService: ScrapingService,
    private errorService: ErrorService,
    private snackBar: MatSnackBar,
  ) {
    this.form = this.fb.group({
      oldCheckbox: [false],
      wbCheckbox: [false],
      ofacCheckbox: [false]
    });
  }

  onCheckboxChange() {
    this.buttonEnabled = Object.values(this.form.value).some(val => val);
  }

  onSubmit() {
    this.fetchTablesData();
  }

  fetchTablesData() {
    this.buttonEnabled = false;
    this.loading = true;
    this.scrapingService.getScrapedData(this.supplierName, this.form.value).subscribe({
      next: response => {
        for (let result of response) {
          switch (result.source) {
            case "Offshore Leaks Database":
              this.oldDataSource = new MatTableDataSource(result.scrapeData as OLDData[]);
              this.oldDataSource.paginator = this.oldDataPaginator;
              this.oldDataSource.sort = this.oldDataSort;
              break;
            case "The World Bank":
              this.wbDataSource = new MatTableDataSource(result.scrapeData as WBData[]);
              this.wbDataSource.paginator = this.wbDataPaginator;
              this.wbDataSource.sort = this.wbDataSort;
              break;
            case "OFAC":
              this.ofacDataSource = new MatTableDataSource(result.scrapeData as OFACData[]);
              this.ofacDataSource.paginator = this.ofacDataPaginator;
              this.ofacDataSource.sort = this.ofacDataSort;
              break;
            default:
              break;
          }
        }
        this.buttonEnabled = true;
        this.loading = false; 
      },
      error: error => {
        let errorMessage;
        if (error.message === UNAUTHORIZED) {
          errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
        } else {
          errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
        }
        this.snackBar.open(errorMessage, '', { duration: 3000 });
        this.buttonEnabled = true;
        this.loading = false;
      }
    }
    )
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
