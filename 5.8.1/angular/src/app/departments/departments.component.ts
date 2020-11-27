import { EditDepartmentComponent } from './edit-department/edit-department.component';
import {  DepartmentsServiceProxy, DepartmentDto, DepartmentDtoPagedResultDto } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';

class PagedDepartmentsRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css'],
  animations: [appModuleAnimation()]
})
export class DepartmentsComponent 
  extends PagedListingComponentBase<DepartmentDto>{
  
  // protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
  //   throw new Error('Method not implemented.');
  // }
  departments: DepartmentDto[] = [];
  keyword = '';
  isActive: boolean | null=true;
  advancedFiltersVisible = true;

  constructor(
    injector: Injector,
    private _departmentService: DepartmentsServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }
  // constructor(
  //   private _departmentService: DepartmentsServiceProxy,
  //   private _modalService: BsModalService
  // ) {
  //   // super(injector);
  // }

  list(
    request: PagedDepartmentsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._departmentService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: DepartmentDtoPagedResultDto) => {
        this.departments = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(department: DepartmentDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', department.departmentName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._departmentService
            .delete(department.id)
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

  createDepartment(): void {
    this.showCreateOrEditDepartmentDialog();
  }

  editDepartment(department: DepartmentDto): void {
    this.showCreateOrEditDepartmentDialog(department.id);
  }
  
  showCreateOrEditDepartmentDialog(id?: number): void {
    let createOrEditDepartmentDialog: BsModalRef;
    if (!id) {
      createOrEditDepartmentDialog = this._modalService.show(
        CreateDepartmentComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditDepartmentDialog = this._modalService.show(
        EditDepartmentComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditDepartmentDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
  // public resetPassword(department: GetDepartmentOutput): void {
  //   this.showResetPasswordUserDialog(user.id);
  // }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = true;
    this.getDataPage(1);
  }

  
 

  // protected delete(department: GetDepartmentOutput): void {
  //   abp.message.confirm(
  //     this.l('UserDeleteWarningMessage', department.departmentName),
  //     undefined,
  //     (result: boolean) => {
  //       if (result) {
  //         this._departmentService.delete(department.id).subscribe(() => {
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
