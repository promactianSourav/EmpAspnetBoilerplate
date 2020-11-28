import { AbpSessionService } from 'abp-ng2-module';
import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto,
  DepartmentsServiceProxy,
  DepartmentDto
} from '@shared/service-proxies/service-proxies';
import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';
import { Session } from 'inspector';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './users.component.html',
  animations: [appModuleAnimation()]
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {
  users: UserDto[] = [];
  departmentlist: DepartmentDto[] = [];
  departmentIdAllowed:number = null;
  rolesAllowedList:string[] = [];
  allowedAdmin:boolean = false;
  allowedHr:boolean = false;
  allowedEmployee:boolean = false;
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _departmentService: DepartmentsServiceProxy,
    private _abpSessionService: AbpSessionService,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createUser(): void {
    if(this.allowedAdmin || this.allowedHr){
      this.showCreateOrEditUserDialog();
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
  
  }

  editUser(user: UserDto): void {
    if(this.allowedAdmin || this.allowedHr){
      this.showCreateOrEditUserDialog(user.id);
    }else if(this._abpSessionService.userId === user.id){
      this.showCreateOrEditUserDialog(user.id);
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
   
  }

  public resetPassword(user: UserDto): void {
    if(this.allowedAdmin || this.allowedHr){
      this.showResetPasswordUserDialog(user.id);
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
    
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;
    this._departmentService.listAll().subscribe((result)=>{
      this.departmentlist = result;
    });

    this._userService.get(this._abpSessionService.userId).subscribe((result)=>{
      this.rolesAllowedList = result.roleNames;
      this.departmentIdAllowed = result.departmentId;
      result.roleNames.forEach(element => {
        // console.log(element);
        
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

      // console.log(this.allowedAdmin);
      // console.log(this.allowedHr);
      // console.log(this.allowedEmployee);
    });

    // console.log(this._abpSessionService.userId);
    // console.log(typeof(this._abpSessionService.userId));
    
    
    
    this._userService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: UserDtoPagedResultDto) => {
        this.users = result.items;
       
        this.showPaging(result, pageNumber);
      });
  }

  protected delete(user: UserDto): void {
    if(this.allowedAdmin || this.allowedHr){
      abp.message.confirm(
        this.l('UserDeleteWarningMessage', user.fullName),
        undefined,
        (result: boolean) => {
          if (result) {
            this._userService.delete(user.id).subscribe(() => {
              abp.notify.success(this.l('SuccessfullyDeleted'));
              this.refresh();
            });
          }
        }
      );
    }else{
      abp.message.info("Sorry! You are not allowed.")
    }
    
  }

  private showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: 'modal-lg',
      initialState: {
        id: id,
      },
    });
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateUserDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditUserDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
