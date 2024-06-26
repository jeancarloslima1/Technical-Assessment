import { NgModule } from '@angular/core';
import { SupplierComponent } from './supplier.component';
import { SupplierListComponent } from './components/supplier-list/supplier-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SupplierFormComponent } from './components/supplier-form/supplier-form.component';
import { SupplierRoutingModule } from './supplier-routing.module';
import { SupplierDialogComponent } from './components/supplier-dialog/supplier-dialog.component';

@NgModule({
  declarations: [
    SupplierComponent,
    SupplierListComponent,
    SupplierDialogComponent,
    SupplierFormComponent
  ],
  imports: [
    SharedModule,
    SupplierRoutingModule
  ]
})
export class SupplierModule { }
