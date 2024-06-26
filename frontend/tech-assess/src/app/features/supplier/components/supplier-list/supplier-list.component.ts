import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { Country } from 'src/app/shared/interfaces/country.model';
import { CountryService } from 'src/app/shared/services/country.service';
import { Supplier } from '../../interfaces/supplier.model';
import { Filter } from '../../interfaces/filter.model';
import { SupplierService } from '../../services/supplier.service';
import { SupplierDialogComponent } from '../supplier-dialog/supplier-dialog.component';
import { Router } from '@angular/router';
import { ConfirmationDialogComponent } from 'src/app/shared/components/comfirmation-dialog/confirmation-dialog.component';
import { ErrorService } from 'src/app/shared/services/error.service';
import { ErrorCode } from 'src/app/shared/interfaces/error-code.enum';
import { UNAUTHORIZED } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {
  displayedColumns: string[] = ['legalName', 'tradeName', 'taxIdentificationNumber', 'phoneNumber', 'email', 'website', 'physicalAddress', 'country', 'annualRevenueUSD', 'actions'];
  dataSource!: MatTableDataSource<Supplier>;
  totalRecords = 0;
  filters: Filter = {
    legalName: '',
    tradeName: '',
    taxIdentificationNumber: '',
    countryId: 0
  };
  countries: Country[] = [];
  pageSize = 10;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private supplierService: SupplierService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private countryService: CountryService,
    private router: Router,    
    private errorService: ErrorService
  ) { }

  ngOnInit(): void {
    this.getCountries();
    this.getSuppliers();
  }

  resetFilters() {
    this.filters = {
      legalName: '',
      tradeName: '',
      taxIdentificationNumber: '',
      countryId: 0
    };
  }

  getCountries() {
    this.countryService.getCountries().subscribe(data => {
      this.countries = data;
    })
  }

  getSuppliers() {
    let pageIndex = this.paginator?.pageIndex ?? 0;
    let pageSize = this.paginator?.pageSize ?? this.pageSize;

    this.supplierService.getSuppliers(pageIndex + 1, pageSize, this.filters).subscribe({
      next: data => {
        this.dataSource = new MatTableDataSource<Supplier>(data.items);
        this.dataSource.sort = this.sort;
        this.totalRecords = data.totalItems;
      },
      error: error => {
        let errorMessage;
        if(error.message === UNAUTHORIZED) {
          errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
        } else{
          errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
        } 
        this.snackBar.open(errorMessage, '', { duration: 3000 });        
      }
    });
  }

  applyFilters() {
    this.paginator.pageIndex = 0;
    this.getSuppliers();
  }

  pageChange(event: any) {
    this.getSuppliers();
  }

  newSupplier(): void {
    this.router.navigate(['/supplier/new']);
  }

  viewSupplier(id: number): void {
    this.router.navigate(['/supplier', id, 'details']);
  }

  editSupplier(id: number): void {
    this.router.navigate(['/supplier', id, 'edit']);
  }

  deleteSupplier(id: number) {

    const dialogRef = this.dialog.open(ConfirmationDialogComponent);
    

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.supplierService.deleteSupplier(id.toString()).subscribe({
          next: response => {
            if (response.errorCode) {
              let errorMessage = this.errorService.getErrorMessage(parseInt(response.errorCode));
              this.snackBar.open(errorMessage, '', { duration: 3000 });
            }
            this.snackBar.open('Supplier deleted successfully', '', { duration: 3000 });
              this.getSuppliers();
          },
          error: error => {
            let errorMessage;
            if(error.message === UNAUTHORIZED) {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
            } else{
              errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
            } 
            this.snackBar.open(errorMessage, '', { duration: 3000 });        
          }
        });
      }
    });
  }

  screening(supplier: Supplier) {
    this.dialog.open(SupplierDialogComponent, {
      data: supplier.legalName
    });
  }
}
