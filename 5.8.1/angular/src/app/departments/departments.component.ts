import { EditDepartmentComponent } from './edit-department/edit-department.component';
import {  DepartmentsServiceProxy, DepartmentDto, DepartmentDtoPagedResultDto, UserServiceProxy } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';
import { AbpSessionService } from 'abp-ng2-module';
import { result } from 'lodash-es';

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
  rolesAllowedList:string[] = [];
  allowedAdmin:boolean = false;
  allowedHr:boolean = false;
  allowedEmployee:boolean = false;
  keyword = '';
  isActive: boolean | null=true;
  advancedFiltersVisible = true;

  constructor(
    injector: Injector,
    private _departmentService: DepartmentsServiceProxy,
    private _userService: UserServiceProxy,
    private _abpSessionService: AbpSessionService,
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

    this._userService.get(this._abpSessionService.userId).subscribe((result)=>{
      this.rolesAllowedList = result.roleNames;
      result.roleNames.forEach(element => {
        if(element.toLocaleLowerCase() == "admin"){
          this.allowedAdmin = true;
        }
        if(element.toLocaleLowerCase() == "hr"){
          this.allowedHr = true;
        }
        if(element.toLocaleLowerCase() == "employee"){
          this.allowedEmployee = true;
        }
      });
    })


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

    if(this.allowedAdmin){
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
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
    
  }

  createDepartment(): void {
    if(this.allowedAdmin){
      this.showCreateOrEditDepartmentDialog();
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
    
  }

  editDepartment(department: DepartmentDto): void {
    if(this.allowedAdmin){
      this.showCreateOrEditDepartmentDialog(department.id);
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
  
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
