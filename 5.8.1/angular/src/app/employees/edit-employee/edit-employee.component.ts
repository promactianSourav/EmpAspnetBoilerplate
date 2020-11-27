import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { EmployeeDto, EmployeesServiceProxy, PermissionEmployeeDto } from '@shared/service-proxies/service-proxies';

import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styles: [
  ]
})
export class EditEmployeeComponent extends AppComponentBase
implements OnInit {
saving = false;
id: number;
employee = new EmployeeDto();
permissions: PermissionEmployeeDto[];
grantedPermissionNames: string[];
checkedPermissionsMap: { [key: string]: boolean } = {};

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  private _employeeService: EmployeesServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._employeeService
    .get(this.id)
    .subscribe((result) => {
      this.employee = result;
      // this.permissions = result.permissions;
      // this.grantedPermissionNames = result.grantedPermissionNames;
      // this.setInitialPermissionsStatus();
    });
}

setInitialPermissionsStatus(): void {
  _map(this.permissions, (item) => {
    this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
      item.name
    );
  });
}

isPermissionChecked(permissionName: string): boolean {
  return _includes(this.grantedPermissionNames, permissionName);
}

onPermissionChange(permission: PermissionEmployeeDto, $event) {
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

  const Employee = new EmployeeDto();
  Employee.init(this.employee);
  // role.grantedPermissions = this.getCheckedPermissions();

  this._employeeService
    .update(Employee)
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

