import { DepartmentsServiceProxy } from './../../shared/service-proxies/service-proxies';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { DepartmentDto, EmployeeDto, EmployeeDtoPagedResultDto, EmployeesServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { result } from 'lodash-es';
class PagedEmployeesRequestDto extends PagedRequestDto {
  keyword: string;
}
@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  animations: [appModuleAnimation()],
  styles: [
  ]
})
export class EmployeesComponent extends PagedListingComponentBase<EmployeeDto>{
  
  
  employees: EmployeeDto[] = [];
  departmentlist: DepartmentDto[] = [];
  keyword = '';
  isActive: boolean | null=true;
  advancedFiltersVisible = true;

  constructor(
    injector: Injector,
    private _employeeService: EmployeesServiceProxy,
    private _departmentService: DepartmentsServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }
  // constructor(
  //   private _EmployeeService: EmployeesServiceProxy,
  //   private _modalService: BsModalService
  // ) {
  //   // super(injector);
  // }

  list(
    request: PagedEmployeesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._departmentService.listAll().subscribe((result)=>{
      this.departmentlist = result;
    })
    this._employeeService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: EmployeeDtoPagedResultDto) => {
        this.employees = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(employee: EmployeeDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', employee.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._employeeService
            .delete(employee.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createEmployee(): void {
    this.showCreateOrEditEmployeeDialog();
  }

  editEmployee(Employee: EmployeeDto): void {
    this.showCreateOrEditEmployeeDialog(Employee.id);
  }
  
  showCreateOrEditEmployeeDialog(id?: number): void {
    let createOrEditEmployeeDialog: BsModalRef;
    if (!id) {
      createOrEditEmployeeDialog = this._modalService.show(
        CreateEmployeeComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditEmployeeDialog = this._modalService.show(
        EditEmployeeComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditEmployeeDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
  // public resetPassword(Employee: GetEmployeeOutput): void {
  //   this.showResetPasswordUserDialog(user.id);
  // }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = true;
    this.getDataPage(1);
  }

  
 

  // protected delete(Employee: GetEmployeeOutput): void {
  //   abp.message.confirm(
  //     this.l('UserDeleteWarningMessage', Employee.EmployeeName),
  //     undefined,
  //     (result: boolean) => {
  //       if (result) {
  //         this._EmployeeService.delete(Employee.id).subscribe(() => {
  //           abp.notify.success(this.l('SuccessfullyDeleted'));
  //           this.refresh();
  //         });
  //       }
  //     }
  //   );
  // }

  // private showResetPasswordUserDialog(id?: number): void {
  //   this._modalService.show(ResetPasswordDialogComponent, {
  //     class: 'modal-lg',
  //     initialState: {
  //       id: id,
  //     },
  //   });
  // }

  
}

