document.addEventListener('DOMContentLoaded', () => {
    const loginContainer = document.getElementById('login-container');
    const createUserContainer = document.getElementById('create-user-container');
    const userContainer = document.getElementById('user-container');
    const checklistContainer = document.getElementById('checklist-container');
    const checklistCreate = document.getElementById('checklist-create');
    const checklistView = document.getElementById('checklist-view');
    const birdView = document.getElementById('bird-view');
    const userManagement = document.getElementById('user-management-button');
    const checklistManagement = document.getElementById('checklist-management-button');

    const createUserButton = document.getElementById('create-user-button'); //Create User button on Login Screen
    const submitUserButton = document.getElementById('submit-user-button');
    const viewListButton = document.getElementById('view-list-button');
    const createListButton = document.getElementById('create-list-button');
    const welcomeMessage = document.getElementById('welcome-message');
    //For checklist-create view
    const checklistSubmit = document.getElementById('checklist-submit');
    const backChecklistManagement = document.getElementById('back-checklist-management');
    //For bird-view
    const submitRecord = document.getElementById('bird-submit');
    const backChecklistManagement2 = document.getElementById('back-checklist-management2');

    const loginButton = document.getElementById('login-button');
    const logoutButton = document.getElementById('logout-button');

    //Adding code to check to see if user is logged in 
    const storedUser = JSON.parse(localStorage.getItem('user'));
    if (storedUser) { //if storedUser is not empty
        updateUIForLoggedInUser(storedUser);
    }

    const currChecklist = JSON.parse(localStorage.getItem('checklist'));

    /////////////Log-In User Functionality//////////////////

    loginButton.addEventListener('click', async () => {
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        if (username) {
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

                localStorage.setItem('user', JSON.stringify(user)); //Just adding that local storage piece in case we want to leverage it

            } catch (error) {
                console.error('Error logging in:', error);
            }
        }
    });//end loginclick


    /////////////Create User Functionality//////////////////

    createUserButton.addEventListener('click', async () => {


        updateUIForCreateUser();
       submitUserButton.addEventListener('click', async() =>{
        const createEmail = document.getElementById('createEmail').value;
        const createUsername = document.getElementById('createUsername').value;
        const createPassword = document.getElementById('createPassword').value;
        if (createUsername) {
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

                localStorage.setItem('user', JSON.stringify(user)); //Again, adding that local storage piece in case we want to leverage it

            } catch (error) {
                console.error('Error Creating Account: ', error);
            }
        }});//end submitUserClick
    });//end createUserClick

    checklistManagement.addEventListener('click', async() =>{
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForChecklistManagement(user);
    })

    userManagement.addEventListener('click', async() =>{
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForUserManagement(user);
    })

    createListButton.addEventListener('click', async() =>{
        const user = JSON.parse(localStorage.getItem('user'));
        updateUIForCreateChecklist(user);
    })

    checklistSubmit.addEventListener('click',async() =>{
        const listDate = document.getElementById('date-seen').value;
        const location = document.getElementById('where').value;
        const user = JSON.parse(localStorage.getItem('user'));
        if(listDate && location)
            {
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
                const currentChecklist = await response.json;
                console.log(currentChecklist.checklistID);
                //localStorage.removeItem('currChecklist');
                localStorage.setItem('currChecklist', JSON.stringify(currentChecklist));
                updateUIForBirdRecords(currentChecklist);
            }
       
    })

    submitRecord.addEventListener('click',async() =>{
        const _numSeen = document.getElementById('count').value;
        const _bird = document.getElementById('select-bird').value;
        const checklist = JSON.parse(localStorage.getItem('currChecklist'));
        if(_numSeen && _bird)
            {
                const response = await fetch(`http://localhost:5066/Birds/AddBird`, 
                {
                    method: "POST",
                    body: JSON.stringify({
                        checklistID: checklist.checklistId,
                        speciesName: _bird,
                        numSeen: _numSeen
                        }),
                    headers: {
                        'content-type': 'application/json'//; 'charset=utf-8 
                    }
                });
                const bird = await response.json
            }
    })

    //////View Checklist Functionality///////////
    viewListButton.addEventListener('click', async() =>{
        updateUIForViewChecklist();
        const user = JSON.parse(localStorage.getItem('user'));
        fetchUserLists(user.userId);
    });//end ViewlistClick


    function updateUIForCreateUser() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        welcomeMessage.textContent = `Welcome ${user.displayName}!`;

        createUserContainer.style.display = 'block';   // What is this doing?
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';

        

    };//end updateUIForCreateUser
    function updateUIForLoggedInUser(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'none';
        userContainer.style.display ='block';

        welcomeMessage.textContent = `Welcome ${user.displayName}!`;

        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';

        

    };//end updateUIForLoggedInUser

    function updateUIForUserManagement(user){
        loginContainer.style.display = 'none';
        userContainer.style.display ='none';
        welcomeMessage.textContent = `Welcome ${user.displayName}!`;
        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';    
    }; //end updateUIForUserManagement

    function updateUIForChecklistManagement(user){
        loginContainer.style.display = 'none';
        userContainer.style.display ='none';
        welcomeMessage.textContent = `Welcome ${user.displayName}!`;
        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'block';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'none';    
    }; //end updateUIForChecklistManagement

    function updateUIForCreateChecklist(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'block';
        checklistView.style.display = 'none';

        

    };//end updateUIForCreateChecklist
    function updateUIForBirdRecords(checklist){
        checklistCreate.style.display = 'none';
        birdView.style.display = 'block';
    }
    function updateUIForViewChecklist() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        //welcomeMessage.textContent = `Welcome ${user.displayName}!`;

        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checklistCreate.style.display = 'none';
        checklistView.style.display = 'block';

        

    };//end updateUIForviewChecklist
    /////////////////////////Logout Handling/////////////

    logoutButton.addEventListener('click', () => {

        localStorage.removeItem('user');  //Deleting local storage of the 'user' object due to

        loginContainer.style.display = 'block';  //Redisplay the login container

        userContainer.style.display = 'none'; //Need to see if we have to call out each container that has the logout functionality available
    });//end of the logoutButton event listener
    /////fetch lists/////
    async function fetchUserLists(userId){
        //this will fetch the checklist from back end may need updating since list contains a list
        try{
            const response = await fetch(`http://localhost:5066/Checklists/ListChecklist/${userId}`);
           
            const list = await response.json();

            renderList(list);
        }
        catch (error){
            console.error('Error Fetching list: ', error)
        }

    };//end Fetchlist

    async function renderList(list){
        list.innerHTML = '';

        list.forEach(item => {
            const listItem = document.createElement('li');

            listItem.textContent = `${list.checklistDateTime} - ${list.locationName}`;

            //itemsList.appendChild(listItem);

        });
    }

});//end DOMContentLoaded