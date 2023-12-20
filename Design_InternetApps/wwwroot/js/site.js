

//базовый адрес для получения списка
const uri = 'api/employee';

//функция для получения всего списка в формате json
function getItemsTableEmployees() {
    fetch(uri)
        .then(responce => responce.json())
        .then(data => displayTableEmployees(data))
}
//функция для отображения сотрубников в таблице c кнопкой удалить
function displayTableEmployees(employees) {
    let emps = document.querySelector("#employees");
    let out = "";
    for (let employee of employees) {
        out += `
                <tr>
                <td>${employee.firstName}</td>
                <td>${employee.lastName}</td>
                <td>${employee.phoneNumber}</td>
                <td><button onclick="deleteEmployee(${employee.id})">удалить</button></td>
                <td><button onclick="updateEmployee(${employee.id})">изменить</button></td>
                </tr>
                `;
    }
    emps.innerHTML = out;
}
//функция удаления пользователя по id
function deleteEmployee(id) {
    fetch(`${uri}/${id}`,
        { method: 'Delete' })
        .then(getItemsTableEmployees)
}
//функция для добавления нового сотрудника в БД
function Add_and_Update_Employee() {

    const empId = document.getElementById('employee-id').value;
    if (empId == 0) {
        const firstNameTexbox = document.getElementById('add-firstName');
        const lastNameTexbox = document.getElementById('add-LastName');
        const phoneNumberTexbox = document.getElementById('add-PhoneNumber');

        const item = {
            firstName: firstNameTexbox.value.trim(),
            lastName: lastNameTexbox.value.trim(),
            phoneNumber: phoneNumberTexbox.value.trim()
        }
        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
            .then(getItemsTableEmployees)
    }
    else {
        updateEmployee(empId);
    }

}
//функция для обновления сотрудника в БД
function updateEmployee(id) {

    //для начала нужно получить объект json по id
    //и заполнить поля в форме включая скрытое поле hidden
    fetch(`${uri}/${id}`)
        .then(responce => responce.json())
        .then(data => {
            document.getElementById('employee-id').value = data.id;
            document.getElementById('add-firstName').value = data.firstName;
            document.getElementById('add-LastName').value = data.lastName;
            document.getElementById('add-PhoneNumber').value = data.phoneNumber;
        })

    document.getElementById('formAddUpdate').value = 'обновить';

    const empIdHidden = document.getElementById('employee-id');
    const firstNameTexbox = document.getElementById('add-firstName');
    const lastNameTexbox = document.getElementById('add-LastName');
    const phoneNumberTexbox = document.getElementById('add-PhoneNumber');

    const item = {
        id: parseInt(empIdHidden.value),
        firstName: firstNameTexbox.value.trim(),
        lastName: lastNameTexbox.value.trim(),
        phoneNumber: phoneNumberTexbox.value.trim()
    }

    fetch(`${uri}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(getItemsTableEmployees)
}




