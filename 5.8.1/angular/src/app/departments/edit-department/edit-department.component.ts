import { DepartmentDto, DepartmentsServiceProxy, PermissionDepartmentDto } from './../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styles: [
  ]
})
export class EditDepartmentComponent extends AppComponentBase
implements OnInit {
saving = false;
id: number;
department = new DepartmentDto();
permissions: PermissionDepartmentDto[];
grantedPermissionNames: string[];
checkedPermissionsMap: { [key: string]: boolean } = {};

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
    .get(this.id)
    .subscribe((result) => {
      this.department = result;
      // this.permissions = result.permissions;
      // this.grantedPermissionNames = result.grantedPermissionNames;
      // this.setInitialPermissionsStatus();
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
  return _includes(this.grantedPermissionNames, permissionName);
}

onPermissionChange(permission: PermissionDepartmentDto, $event) {
  this.checkedPermissionsMap[permission.departmentName] = $event.target.checked;
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

  const department = new DepartmentDto();
  department.init(this.department);
  // role.grantedPermissions = this.getCheckedPermissions();

  this._departmentService
    .update(department)
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

