import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SupplierService } from '../../services/supplier.service';
import { Country } from 'src/app/shared/interfaces/country.model';
import { CountryService } from 'src/app/shared/services/country.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from 'src/app/shared/services/error.service';
import { ErrorCode } from 'src/app/shared/interfaces/error-code.enum';
import { UNAUTHORIZED } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-supplier-form',
  templateUrl: './supplier-form.component.html',
  styleUrls: ['./supplier-form.component.css']
})
export class SupplierFormComponent implements OnInit {

  supplierForm!: FormGroup;
  isReadOnly: boolean = false;
  supplierId: string | null = null;
  countries: Country[] = [];

  constructor(
    private fb: FormBuilder,
    private supplierService: SupplierService,
    private snackBar: MatSnackBar,
    private countryService: CountryService,
    private route: ActivatedRoute,
    private router: Router,
    private errorService: ErrorService
  ) { }

  ngOnInit(): void {
    this.getCountries();

    this.supplierForm = this.fb.group({
      legalName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9&\\-_\\.!, ]+$')]],
      tradeName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9&\\-_\\.!, ]+$')]],
      taxIdentificationNumber: ['', [Validators.required, Validators.pattern('^\\d{11}$')]],
      phoneNumber: ['', [Validators.required, Validators.min(7), Validators.pattern('^\\+\\d{7,15}$')]],
      email: ['', [Validators.required, Validators.email]],
      website: ['', [Validators.required, Validators.pattern('^(http|https):\\/\\/[a-zA-Z0-9\\-\\.]+\\.[a-z]{2,4}(\\/.*)?$')]],
      physicalAddress: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9&\\-_\\.!, ]+$')]],
      countryId: ['', Validators.required],
      annualRevenueUSD: [null, [Validators.required, Validators.min(0)]]
    });

    this.supplierForm.get("phoneNumber")!.valueChanges.subscribe(value => {
      if (value && !isNaN(value[0])) {
        this.supplierForm.get("phoneNumber")!.setValue(`+${value}`);
      }
    });

    this.route.paramMap.subscribe(params => {
      this.supplierId = params.get('id');
      if (this.supplierId) {
        this.isReadOnly = this.route.snapshot.url.at(1)?.path === 'details';
        if (this.isReadOnly) {
          this.supplierForm.disable();
        }
        
        this.supplierService.getSupplier(this.supplierId).subscribe({
          next: response => {
            if (response.errorCode) {
              let errorMessage = this.errorService.getErrorMessage(parseInt(response.errorCode));
              this.snackBar.open(errorMessage, '', { duration: 3000 });
            }
            this.supplierForm.patchValue(response.data!);
          },
          error: error => {
            let errorMessage;
            if (error.message === UNAUTHORIZED) {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
            } else {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
            }
            this.snackBar.open(errorMessage, '', { duration: 3000 });
          }
        });
      }
    });
  }

  getCountries() {
    this.countryService.getCountries().subscribe(data => {
      this.countries = data;
    })
  }

  onSubmit() {
    if (this.supplierForm.valid) {
      if (this.supplierId) {
        this.supplierService.updateSupplier(this.supplierId, { id: this.supplierId, ...this.supplierForm.value }).subscribe({
          next: response => {
            if (response.errorCode) {
              let errorMessage = this.errorService.getErrorMessage(parseInt(response.errorCode));
              this.snackBar.open(errorMessage, '', { duration: 3000 });
            }
            this.snackBar.open('Supplier updated successfully', '', { duration: 3000 });
            this.router.navigate(['/supplier']);
          },
          error: error => {
            let errorMessage;
            if (error.message === UNAUTHORIZED) {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
            } else {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
            }
            this.snackBar.open(errorMessage, '', { duration: 3000 });
          }
        });
      } else {
        this.supplierService.createSupplier(this.supplierForm.value).subscribe({
          next: response => {
            if (response.errorCode) {
              let errorMessage = this.errorService.getErrorMessage(parseInt(response.errorCode));
              this.snackBar.open(errorMessage, '', { duration: 3000 });
            }
            this.snackBar.open('Supplier created successfully', '', { duration: 3000 });
            this.router.navigate(['/supplier']);
          },
          error: error => {
            let errorMessage;
            if (error.message === UNAUTHORIZED) {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.Unauthorized);
            } else {
              errorMessage = this.errorService.getErrorMessage(ErrorCode.InternalServerError);
            }
            this.snackBar.open(errorMessage, '', { duration: 3000 });
          }
        });
      }
    }
  }


  goBack(){
    this.router.navigate(['/supplier']);
  }
}
