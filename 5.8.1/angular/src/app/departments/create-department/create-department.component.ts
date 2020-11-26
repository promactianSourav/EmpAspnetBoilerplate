import { DepartmentsServiceProxy, GetDepartmentOutput } from './../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';
import { CreateDepartmentInput, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styles: [
  ]
})
export class CreateDepartmentComponent extends AppComponentBase
implements OnInit {
saving = false;
Department = new CreateDepartmentInput();
departments: GetDepartmentOutput[] = [];
// roles: RoleDto[] = [];
checkedRolesMap: { [key: string]: boolean } = {};
defaultRoleCheckedStatus = false;
passwordValidationErrors: Partial<AbpValidationError>[] = [
  {
    name: 'pattern',
    localizationKey:
      'PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber',
  },
];
confirmPasswordValidationErrors: Partial<AbpValidationError>[] = [
  {
    name: 'validateEqual',
    localizationKey: 'PasswordsDoNotMatch',
  },
];

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  public _departmentService: DepartmentsServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  // this.Department.isActive = true;

  this._departmentService.listAll().subscribe((result) =>{
    this.departments = result;
  })
  // this._userService.getRoles().subscribe((result) => {
  //   this.roles = result.items;
  //   this.setInitialRolesStatus();
  // });
}

// setInitialRolesStatus(): void {
//   _map(this.roles, (item) => {
//     this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
//       item.normalizedName
//     );
//   });
// }

// isRoleChecked(normalizedName: string): boolean {
//   // just return default role checked status
//   // it's better to use a setting
//   return this.defaultRoleCheckedStatus;
// }

// onRoleChange(role: RoleDto, $event) {
//   this.checkedRolesMap[role.normalizedName] = $event.target.checked;
// }

// getCheckedRoles(): string[] {
//   const roles: string[] = [];
//   _forEach(this.checkedRolesMap, function (value, key) {
//     if (value) {
//       roles.push(key);
//     }
//   });
//   return roles;
// }

// save(): void {
//   this.saving = true;

//   this.user.roleNames = this.getCheckedRoles();

//   this._userService
//     .create(this.user)
//     .pipe(
//       finalize(() => {
//         this.saving = false;
//       })
//     )
//     .subscribe(() => {
//       this.notify.info(this.l('SavedSuccessfully'));
//       this.bsModalRef.hide();
//       this.onSave.emit();
//     });
// }
}
