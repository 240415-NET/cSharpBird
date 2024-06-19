document.addEventListener('DOMContentLoaded', () => {
    const loginContainer = document.getElementById('login-container');
    const createUserContainer = document.getElementById('create-user-container');
    const userContainer = document.getElementById('user-container');
    const checklistContainer = document.getElementById('checklist-container');
    const checklistCreate = document.getElementById('checklist-create');
    const checklistView = document.getElementById('checklist-view');
    const birdView = document.getElementById('bird-view');
    const userManagement = document.getElementById('user-management-button');
    const userManagementView = document.getElementById('user-management');
    const checklistManagement = document.getElementById('checklist-management-button');
    let mainMenuReturnUpdateUser = document.getElementById('main-menu-return-update-user');  //adding for return to main

    const createPassword = document.getElementById('createPassword');
    const createUserButton = document.getElementById('create-user-button'); //Create User button on Login Screen
    const submitUserButton = document.getElementById('submit-user-button');
    const viewListButton = document.getElementById('view-list-button');
    const createListButton = document.getElementById('create-list-button');
    const welcomeMessage = document.getElementById('welcome-message');

    //For Update User button
    const updateUserButton = document.getElementById('update-user-button');
    const userManagementNothingEntered = document.getElementById('usermanagement-nothing-entered');

    //For checklist-create view
    const checklistSubmit = document.getElementById('checklist-submit');
    const backChecklistManagement = document.getElementById('back-checklist-management');
    const checklistSubmitNothingEntered = document.getElementById('checklist-submit-nothing-entered');
    const checklistsubmitnothingentered2 = document.getElementById('checklist-submit-nothing-entered2');
    const recordAddedToChecklist = document.getElementById('record-added-to-checklist');

    const mainMenuReturnChecklistButton = document.getElementById('main-menu-return-checklist');

    //For bird-view
    const submitRecord = document.getElementById('bird-submit');

    //For checklist-view
    let checklistList = document.getElementById('checklist-list');
    const backChecklistManagement2 = document.getElementById('back-checklist-management2');
    const backChecklistManagement3 = document.getElementById('back-checklist-management3');
    const password = document.getElementById('password');
    const loginButton = document.getElementById('login-button');
    const logoutButton = document.getElementById('logout-button');
    const noUserFoundOnLogin = document.getElementById('login-no-user-found'); //Used for No User Found on Login Message from HTML
    const createUserEmailInUse = document.getElementById('create-user-email-in-use');
    const createUserAllFieldsRequired = document.getElementById('create-user-all-fields-required');
    const loginReturn = document.getElementById('login-return');  //Return to Login Screen Button

    //for Bird list
    let birdList = document.getElementById('bird-list');
    const birdlistView = document.getElementById('bird-list-view');
    const backChecklistManagement4 = document.getElementById('back-checklist-management4');

    const currChecklist = JSON.parse(localStorage.getItem('checklist'));

    //Adding code to check to see if user is logged in 
    const storedUser = JSON.parse(localStorage.getItem('user'));
    if (storedUser) { //if storedUser is not empty
        updateUIForLoggedInUser(storedUser);
    }



    /////////////Log-In User Functionality//////////////////

    ////Event Listener for the Enter Key on the Password Field////

    password.addEventListener('keyup', async (event) => {
        if (event.key === 'Enter') {
            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;

            if (username && password) {
                try {
                    const response = await fetch(`http://localhost:5066/Users/Signin`,
                        {
                            method: "POST",
                            body: JSON.stringify({
                                userName: username,
                                rawPassword: password
                            }),
                            headers: {
                                'content-type': 'application/json'
                            }
                        });


                    const user = await response.json();

                    updateUIForLoggedInUser(user);

                    localStorage.setItem('user', JSON.stringify(user));

                } catch (error) {
                    console.error(error);
                    noUserFoundOnLogin.style.display = 'block'; //Display noUserFoundOnLogin if there is an error trying find existing user
                    setTimeout(() => noUserFoundOnLogin.style.display = 'none', 5000); //Hide noUserFoundOnLogin after 5 seconds
                }
            } else {
                // Display noUserFoundOnLogin if either username or password is left blank
                noUserFoundOnLogin.style.display = 'block';
                setTimeout(() => noUserFoundOnLogin.style.display = 'none', 5000);
            }
        }
    });

    loginButton.addEventListener('click', async () => {
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        if (username && password) {
            try {
                const response = await fetch(`http://localhost:5066/Users/Signin`,
                    {
                        method: "POST",
                        body: JSON.stringify({
                            userName: username,
                            rawPassword: password
                        }),
                        headers: {
                            'content-type': 'application/json'
                        }
                    });

                const user = await response.json();

                updateUIForLoggedInUser(user);

                localStorage.setItem('user', JSON.stringify(user));
                noUserFoundOnLogin.style.display = 'none';

            } catch (error) {
                console.error('Error logging in:', error);
                noUserFoundOnLogin.style.display = 'block';
                setTimeout(() => noUserFoundOnLogin.style.display = 'none', 5000);
            }
        } else {
            // Display noUserFoundOnLogin if either username or password is blank
            noUserFoundOnLogin.style.display = 'block';
            setTimeout(() => noUserFoundOnLogin.style.display = 'none', 5000);
        }
    });//end loginclick




    /////////////Create User Functionality//////////////////

    //Event Listener for the Enter Key on the Create Password Field

    createPassword.addEventListener('keyup', async (event) => {
        if (event.key === 'Enter') {
            const createUsername = document.getElementById('createUsername').value;
            const createEmail = document.getElementById('createEmail').value;
            const createPassword = document.getElementById('createPassword').value;

            if (createUsername && createEmail && createPassword) {
                try {
                    const response = await fetch(`http://localhost:5066/Users/Create`,
                        {
                            method: "POST",
                            body: JSON.stringify({
                                userEmail: createEmail,
                                displayName: createUsername,
                                rawPassword: createPassword
                            }),
                            headers: {
                                'content-type': 'application/json'//; 'charset=utf-8 
                            }
                        });

                    const user = await response.json();

                    updateUIForLoggedInUser(user);

                    localStorage.setItem('user', JSON.stringify(user));
                    createUserEmailInUse.style.display = 'none';
                    createUserAllFieldsRequired.style.display = 'none';

                } catch (error) {
                    console.error('Error logging in:', error);
                    createUserEmailInUse.style.display = 'block';
                    setTimeout(() => createUserEmailInUse.style.display = 'none', 5000);
                    createUserAllFieldsRequired.style.display = 'none';
                }
            }
            else {
                createUserAllFieldsRequired.style.display = 'block';
                setTimeout(() => createUserAllFieldsRequired.style.display = 'none', 5000);
                createUserEmailInUse.style.display = 'none';
            }
        };
    });//end createPasswordEnter

    createUserButton.addEventListener('click', async () => {

        updateUIForCreateUser();
        submitUserButton.addEventListener('click', async () => {
            const createEmail = document.getElementById('createEmail').value;
            const createUsername = document.getElementById('createUsername').value;
            const createPassword = document.getElementById('createPassword').value;
            if (createUsername && createEmail && createPassword) {
                try {
                    //const response = await fetch(`http://localhost:5066/Users/Create/${createEmail},${createUsername},${createPassword}`);
                    const response = await fetch(`http://localhost:5066/Users/Create`,
                        {
                            method: "POST",
                            body: JSON.stringify({
                                userEmail: createEmail,
                                displayName: createUsername,
                                rawPassword: createPassword
                            }),
                            headers: {
                                'content-type': 'application/json'//; 'charset=utf-8 
                            }
                        });
                    const user = await response.json();

                    updateUIForLoggedInUser(user);

                    localStorage.setItem('user', JSON.stringify(user));
                    createUserEmailInUse.style.display = 'none';
                    createUserAllFieldsRequired.style.display = 'none';

                } catch (error) {
                    console.error('Error Creating Account: ', error);
                    createUserEmailInUse.style.display = 'block';
                    setTimeout(() => createUserEmailInUse.style.display = 'none', 5000);
                    createUserAllFieldsRequired.style.display = 'none';
                }
            }
            else {
                createUserAllFieldsRequired.style.display = 'block';
                setTimeout(() => createUserAllFieldsRequired.style.display = 'none', 5000);
                createUserEmailInUse.style.display = 'none';
            }
        });//end submitUserClick
    });//end createUserClick

    // update user button FC
    updateUserButton.addEventListener('click', async () => {
        const updateEmail = document.getElementById('updateEmail').value;
        const updateUsername = document.getElementById('updateUsername').value;
        const updatePassword = document.getElementById('updatePassword').value;
        const userGuid = JSON.parse(localStorage.getItem('user'));
        if (updateUsername || updateEmail || updatePassword) {
            try {
                const response = await fetch(`http://localhost:5066/Users/UpdateUser`,
                    {
                        method: "POST",
                        body: JSON.stringify({
                            userId: userGuid.userId,
                            email: updateEmail,
                            userName: updateUsername,
                            rawPassword: updatePassword
                        }),
                        headers: {
                            'content-type': 'application/json'//; 'charset=utf-8 
                        }
                    });
                const user = await response.json();

                updateUIForLoggedInUser(user);

                localStorage.setItem('user', JSON.stringify(user)); //Again, adding that local storage piece in case we want to leverage it
                userManagementNothingEntered.style.display = 'none';

            } catch (error) {
                console.error('Error updating Account: ', error);
                userManagementNothingEntered.style.display = 'block';
                setTimeout(() => userManagementNothingEntered.style.display = 'none', 5000);
            }
        }
        else {
            userManagementNothingEntered.style.display = 'block';
            setTimeout(() => userManagementNothingEntered.style.display = 'none', 5000);
        }
    });//end updateUserClick
    //});//end updateUserClick

    mainMenuReturnUpdateUser.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForUpdateUser(user);
    })

    function updateUIForUpdateUser(user) {
        userManagementView.style.display = 'none';
        loginContainer.style.display = 'none';
        userContainer.style.display = 'block';
        welcomeMessage.textContent = `Welcome ${user.displayName}!`;
        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';
    };
    //return to login screen
    loginReturn.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        loginUI(user);
    })

    checklistManagement.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForChecklistManagement(user);
    })

    userManagement.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForUserManagement(user);
    })

    createListButton.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForCreateChecklist(user);
    })

    checklistSubmit.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        const location = document.getElementById('where').value;
        const listDate = document.getElementById('date-seen').value;
        checklistSubmitNothingEntered.style.display = 'none';

        if (location && listDate) {
            const response = await fetch(`http://localhost:5066/Checklists/Create`,
                {
                    method: "POST",
                    body: JSON.stringify({
                        userId: user.userId,
                        locationName: location,
                        checklistDateTime: listDate
                    }),
                    headers: {
                        'content-type': 'application/json'//; 'charset=utf-8 
                    }
                });
            const currentChecklist = await response.json();
            localStorage.setItem('currChecklist', JSON.stringify(currentChecklist));
            const check = JSON.parse(localStorage.getItem('currChecklist'));
            updateUIForBirdRecords(currentChecklist);
        }
        else {
            checklistSubmitNothingEntered.style.display = 'block';
            setTimeout(() => checklistSubmitNothingEntered.style.display = 'none', 5000);
        }

    })




    submitRecord.addEventListener('click', async () => {
        const _numSeen = document.getElementById('count').value;
        const _bird = document.getElementById('select-bird').value;
        const checklist = JSON.parse(localStorage.getItem('currChecklist'));
        if (_numSeen && _bird) {
            const response = await fetch(`http://localhost:5066/Birds/AddBird`,
                {
                    method: "POST",
                    body: JSON.stringify({
                        speciesName: _bird,
                        numSeen: _numSeen,
                        checklistID: checklist.checklistID
                    }),
                    headers: {
                        'content-type': 'application/json'//; 'charset=utf-8 
                    }
                });
            const bird = await response.json();
            recordAddedToChecklist.style.display = 'block';
            setTimeout(() => recordAddedToChecklist.style.display = 'none', 5000);
        }
        else {
            checklistsubmitnothingentered2.style.display = 'block';
            setTimeout(() => checklistsubmitnothingentered2.style.display = 'none', 5000);
        }
    });

    backChecklistManagement.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForChecklistManagement(user);
    });

    //This is the Back to Checklist Management button on the checklist-view page
    backChecklistManagement2.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForChecklistManagement(user);
    })
    backChecklistManagement3.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForChecklistManagement(user);
    })
    backChecklistManagement4.addEventListener('click', async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForViewChecklist(user);
    })


    mainMenuReturnChecklistButton.addEventListener('click', async () => {
        user = JSON.parse(localStorage.getItem('user'));
        updateUIForLoggedInUser(user);
    })

    //////View Checklist Functionality///////////
    viewListButton.addEventListener('click', async () => {
        updateUIForViewChecklist();

    });//end ViewlistClick



    //return to login page from create user page function
    function loginUI(user) {

        loginContainer.style.display = 'block';
        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'none';



    };//end updateUIForCreateUser


    //This is the Back to Checklist Management button on the checklist-view page

    function updateUIForCreateUser() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'block';   // What is this doing?
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'none';



    };//end updateUIForCreateUser
    function updateUIForLoggedInUser(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'none';
        userContainer.style.display = 'block';

        welcomeMessage.textContent = `Welcome ${user.displayName}!`;

        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';

        birdlistView.style.display = 'none';

        userManagementView.style.display = 'none';

    };//end updateUIForLoggedInUser

    function updateUIForUserManagement(user) {
        userManagementView.style.display = 'block';
        loginContainer.style.display = 'none';
        userContainer.style.display = 'none';
        welcomeMessage.textContent = `Welcome ${user.displayName}!`;
        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'none';
    }; //end updateUIForUserManagement

    function updateUIForChecklistManagement(user) {
        loginContainer.style.display = 'none';
        userContainer.style.display = 'none';
        welcomeMessage.textContent = `Welcome ${user.displayName}!`;
        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'block';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';
        birdView.style.display = 'none';
        birdlistView.style.display = 'none';

    }; //end updateUIForChecklistManagement


    function updateUIForCreateChecklist(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'block';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'none';



    };//end updateUIForCreateChecklist
    function updateUIForBirdRecords(checklist) {
        checklistCreate.style.display = 'none';
        birdView.style.display = 'block';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'none';
    }
    function updateUIForViewChecklist() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        //welcomeMessage.textContent = `Welcome ${user.displayName}!`;

        createUserContainer.style.display = 'none';
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'block';
        const user = JSON.parse(localStorage.getItem('user'));
        fetchUserLists(user.userId);
        birdlistView.style.display = 'none';


    };//end updateUIForviewChecklist

    function updateUIForBirdview() {
        checklistCreate.style.display = 'none';
        birdView.style.display = 'none';
        checklistView.style.display = 'none';
        birdlistView.style.display = 'block';
    }

    /////////////////////////Logout Handling/////////////

    logoutButton.addEventListener('click', () => {

        localStorage.removeItem('user');  //Deleting local storage of the 'user' object due to

        loginContainer.style.display = 'block';  //Redisplay the login container

        userContainer.style.display = 'none'; //Need to see if we have to call out each container that has the logout functionality available
    });//end of the logoutButton event listener
    /////fetch lists/////
    async function fetchUserLists(userId) {
        //this will fetch the checklist from back end may need updating since list contains a list
        try {
            let response = await fetch(`http://localhost:5066/Checklists/ListChecklist/${userId}`);

            let list = await response.json();
            renderList(list);
            //list = [];
        }
        catch (error) {
            console.error('Error Fetching list: ', error)
        }

    };//end Fetchlist

    function renderList(list) {
        list.innerHTML = ``;
        checklistList.innerHTML = ``;

        list.forEach(list => {


            const listItem = document.createElement('li');


            var addBirdButton = document.createElement('button');
            addBirdButton.textContent = "Update Bird List";
            addBirdButton.value = list.checklistID;
            addBirdButton.addEventListener('click', async () => {
                ClickAddBird(list.checklistID);

            });

            var deleteListButton = document.createElement('button');
            deleteListButton.textContent = "Delete Bird List";
            deleteListButton.value = list.checklistID;
            deleteListButton.addEventListener('click', async () => {
                DeleteList(list.checklistID);
            });
            //deleteListButton.classList({"justify-content" : "right"})

            var birdListButton = document.createElement('button');
            birdListButton.textContent = "View Bird List";
            birdListButton.value = list.checklistID;
            birdListButton.addEventListener('click', async () => {
                ShowThemBirds(list.birds);

            });


            if (list.birds.length > 0) {
                const date = new Date(list.checklistDateTime);
                const formattedDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
                listItem.textContent = `Date: ${formattedDate} - Location: ${list.locationName};\nSpecies: ${list.birds[0].speciesName}; Number Seen: ${list.birds[0].numSeen}`;
            } else {
                const date = new Date(list.checklistDateTime);
                const formattedDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
                listItem.textContent = `Date: ${formattedDate} - Location: ${list.locationName}; No Birds Seen`;
            }

            checklistList.appendChild(listItem);
            checklistList.appendChild(addBirdButton);            
            checklistList.appendChild(birdListButton);
            checklistList.appendChild(deleteListButton);

        });
    }// end RenderList

    async function ClickAddBird(listId) {

        localStorage.removeItem('currChecklist');
        let response = await fetch(`http://localhost:5066/Checklists/GetChecklist/${listId}`);
        let currentChecklist2 = await response.json();
        localStorage.setItem('currChecklist', JSON.stringify(currentChecklist2));
        updateUIForBirdRecords();
    }//end add bird

    async function DeleteList(listId) {
        await fetch(`http://localhost:5066/Checklists/Delete${listId}`, {
            method: 'DELETE',
        });

        updateUIForViewChecklist();

    }//end delete

    function ShowThemBirds(bird) {
        updateUIForBirdview();


        bird.innerHTML = ``;
        birdList.innerHTML = ``;

        bird.forEach(bird => {


            const listItem = document.createElement('li');


            //if (bird.length > 0) {

            listItem.textContent = `Species: ${bird.speciesName} -- Number Seen: ${bird.numSeen}`;
            // } else {

            //  listItem.textContent = `No Birds Seen`;
            // }



            birdList.appendChild(listItem);
        }
        );
    }

});//end DOMContentLoaded