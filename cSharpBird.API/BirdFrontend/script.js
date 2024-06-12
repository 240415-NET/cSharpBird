document.addEventListener('DOMContentLoaded', () => {
    const loginContainer = document.getElementById('login-container');
    const createUserContainer = document.getElementById('create-user-container');
    const userContainer = document.getElementById('user-container');
    const checklistContainer = document.getElementById('checklist-container');
    const checlistCreate = document.getElementById('checklist-create');
    const checlistView = document.getElementById('checklist-view');
    
    const createUserButton = document.getElementById('create-user-button'); //Create User button on Login Screen
    const submitUserButton = document.getElementById('submit-user-button');
    const viewListButton = document.getElementById('view-list-button');
    const createListButton = document.getElementById('create-list-button');

    const loginButton = document.getElementById('login-button');
    const logoutButton = document.getElementById('logout-button');

    //Adding code to check to see if user is logged in 
    const storedUser = JSON.parse(localStorage.getItem('user'));
    if (storedUser) { //if storedUser is not empty
        updateUIForLoggedInUser(storedUser);
    }

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
                        'content-type': 'application/json'//; 'charset=utf-8 
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

        const createEmail = document.getElementById('createEmail').value;
        const createUsername = document.getElementById('createUsername').value;
        const createPassword = document.getElementById('createPassword').value;
        updateUIForCreateUser();
       submitUserButton.addEventListener('click', async() =>{
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

    //////View Checklist Functionality///////////
    viewListButton.addEventListener('click', async() =>{
        updateUIForViewChecklist();




    });//end ViewlistClick

    function updateUIForCreateUser() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        //welcomeMessage.textContent = `Welcome ${user.userName}!`;

        createUserContainer.style.display = 'block';   // What is this doing?
        checklistContainer.style.display = 'none';
        checlistCreate.style.display = 'none';
        checlistView.style.display = 'none';

        

    };//end updateUIForCreateUser
    function updateUIForLoggedInUser(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        welcomeMessage.textContent = `Welcome ${user.userName}!`;

        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'block';
        checlistCreate.style.display = 'none';
        checlistView.style.display = 'none';

        

    };//end updateUIForLoggedInUser
    function updateUIForCreateChecklist(user) {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        welcomeMessage.textContent = `Welcome ${user.userName}!`;

        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checlistCreate.style.display = 'block';
        checlistView.style.display = 'none';

        

    };//end updateUIForCreateChecklist
    function updateUIForViewChecklist() {  //Not sure we need to include this function here again or just call it from above

        loginContainer.style.display = 'none';

        //welcomeMessage.textContent = `Welcome ${user.userName}!`;

        createUserContainer.style.display = 'none';   
        checklistContainer.style.display = 'none';
        checlistCreate.style.display = 'none';
        checlistView.style.display = 'block';

        

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
            const response = await fetch(`http://localhost:5066/Checklists/ListChecklist${userId}`);
           
            const list = await response.json();

            renderList(list);
        }
        catch (error){
            console.error('Error Fetching list: ', error)
        }

    };//end Fetchlist

    async function renderList(list){
        //this will take the list passed in by the fetch function 
        //and display the information on it

    }

});//end DOMContentLoaded