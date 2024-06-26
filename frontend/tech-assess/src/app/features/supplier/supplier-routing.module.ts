import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierComponent } from './supplier.component';
import { SupplierFormComponent } from './components/supplier-form/supplier-form.component';
import { AuthGuard } from 'src/app/guards/auth.guard';

const supplierRoutes: Routes = [
  { path: '', component: SupplierComponent, canActivate: [AuthGuard] },
  { path: 'new', component: SupplierFormComponent, canActivate: [AuthGuard] },
  { path: ':id/details', component: SupplierFormComponent, canActivate: [AuthGuard] },
  { path: ':id/edit', component: SupplierFormComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(supplierRoutes)],
  exports: [RouterModule]
})
export class SupplierRoutingModule { }