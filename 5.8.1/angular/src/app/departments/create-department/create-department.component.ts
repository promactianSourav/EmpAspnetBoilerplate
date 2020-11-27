import { DepartmentsServiceProxy, DepartmentDto, PermissionDto, PermissionDepartmentDto } from './../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';
import { CreateDepartmentInput, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

import { forEach as _forEach, map as _map } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styles: [
  ]
})
export class CreateDepartmentComponent extends AppComponentBase
implements OnInit {
saving = false;
department = new DepartmentDto();
permissions: PermissionDepartmentDto[] = [];
checkedPermissionsMap: { [key: string]: boolean } = {};
defaultPermissionCheckedStatus = true;

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  private _departmentService: DepartmentsServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._departmentService
    .getAllPermissions()
    .subscribe((result) => {
      this.permissions = result.items;
      this.setInitialPermissionsStatus();
    });
}

setInitialPermissionsStatus(): void {
  _map(this.permissions, (item) => {
    this.checkedPermissionsMap[item.departmentName] = this.isPermissionChecked(
      item.departmentName
    );
  });
}

isPermissionChecked(permissionName: string): boolean {
  // just return default permission checked status
  // it's better to use a setting
  return this.defaultPermissionCheckedStatus;
}

onPermissionChange(permission: PermissionDto, $event) {
  this.checkedPermissionsMap[permission.name] = $event.target.checked;
}

getCheckedPermissions(): string[] {
  const permissions: string[] = [];
  _forEach(this.checkedPermissionsMap, function (value, key) {
    if (value) {
      permissions.push(key);
    }
  });
  return permissions;
}

save(): void {
  this.saving = true;

  const department = new CreateDepartmentInput();
  department.init(this.department);
  // role.grantedPermissions = this.getCheckedPermissions();

  this._departmentService
    .create(department)
    .pipe(
      finalize(() => {
        this.saving = false;
      })
    )
    .subscribe(() => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    });
}
}
