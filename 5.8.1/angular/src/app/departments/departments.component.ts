import { EditDepartmentComponent } from './edit-department/edit-department.component';
import { GetDepartmentOutput, DepartmentsServiceProxy } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateDepartmentComponent } from './create-department/create-department.component';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent extends PagedListingComponentBase<GetDepartmentOutput> {
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  departments: GetDepartmentOutput[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _departmentService: DepartmentsServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createDepartment(): void {
    this.showCreateOrEditDepartmentDialog();
  }

  editDepartment(department: GetDepartmentOutput): void {
    this.showCreateOrEditDepartmentDialog(department.id);
  }

  // public resetPassword(department: GetDepartmentOutput): void {
  //   this.showResetPasswordUserDialog(user.id);
  // }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  // protected list(
  //   request: PagedUsersRequestDto,
  //   pageNumber: number,
  //   finishedCallback: Function
  // ): void {
  //   request.keyword = this.keyword;
  //   request.isActive = this.isActive;

  //   this._userService
  //     .getAll(
  //       request.keyword,
  //       request.isActive,
  //       request.skipCount,
  //       request.maxResultCount
  //     )
  //     .pipe(
  //       finalize(() => {
  //         finishedCallback();
  //       })
  //     )
  //     .subscribe((result: UserDtoPagedResultDto) => {
  //       this.users = result.items;
  //       this.showPaging(result, pageNumber);
  //     });
  // }

  protected delete(department: GetDepartmentOutput): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', department.departmentName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._departmentService.delete(department.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  // private showResetPasswordUserDialog(id?: number): void {
  //   this._modalService.show(ResetPasswordDialogComponent, {
  //     class: 'modal-lg',
  //     initialState: {
  //       id: id,
  //     },
  //   });
  // }

  private showCreateOrEditDepartmentDialog(id?: number): void {
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
}
