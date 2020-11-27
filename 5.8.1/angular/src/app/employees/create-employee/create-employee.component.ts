import { DepartmentDto, DepartmentsServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateEmployeeInput, EmployeeDto, EmployeesServiceProxy, PermissionDto, PermissionEmployeeDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styles: [
  ]
})
export class CreateEmployeeComponent extends AppComponentBase
implements OnInit {
saving = false;
employee = new EmployeeDto();
departmentlist: DepartmentDto[] = [];
permissions: PermissionEmployeeDto[] = [];
checkedPermissionsMap: { [key: string]: boolean } = {};
defaultPermissionCheckedStatus = true;

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  private _employeeService: EmployeesServiceProxy,
  private _departmentService: DepartmentsServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._employeeService
    .getAllPermissions()
    .subscribe((result) => {
      this.permissions = result.items;
      this.setInitialPermissionsStatus();
    });
    this._departmentService.listAll().subscribe((result) =>{
      this.departmentlist = result;
    })
}

setInitialPermissionsStatus(): void {
  _map(this.permissions, (item) => {
    this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
      item.name
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

  const Employee = new CreateEmployeeInput();
  // this.employee.departmentId = Number(this.employee.departmentId);
  Employee.init(this.employee);
  
  // role.grantedPermissions = this.getCheckedPermissions();

  this._employeeService
    .create(Employee)
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
