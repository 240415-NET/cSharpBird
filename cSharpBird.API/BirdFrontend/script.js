document.addEventListener('DOMContentLoaded', () => {
const loginContainer = document.getElementById('login-container');
const createUserContainer = document.getElementById('create-user-container');
const userContainer = document.getElementById('user-container');
const checklistContainer = document.getElementById('checklist-container');
const checlistCreate = document.getElementById('checklist-create');
const checlistView = document.getElementById('checklist-view');

const createUserButton = document.getElementById('create-user-button'); //Create User button on Login Screen

const loginButton = document.getElementById('login-button');
const logoutButton = document.getElementById('logout-button');

//Adding code to check to see if user is logged in 
const storedUser = JSON.parse(localStorage.getItem('user'));
    if(storedUser) { //if storedUser is not empty
       updateUIForLoggedInUser(storedUser);
    }

/////////////Log-In User Functionality//////////////////

loginButton.addEventListener('click', async () => {
const username = document.getElementById('username').value;
const password= document.getElementById('password').value;

if(username){
    try{
        const response = await fetch(`http://localhost:PORTNUMBER/Users/Signin${username},${password}`);

        const user = await response.json();

        updateUIForLoggedInUser(user);

        localStorage.setItem('user', JSON.stringify(user)); //Just adding that local storage piece in case we want to leverage it

    }catch(error){
        console.error('Error logging in:', error);
    }
}
});//end loginclick

function updateUIForLoggedInUser(user) {  
    loginContainer.style.display = 'none';
   
    welcomeMessage.textContent = `Welcome ${user.userName}!`;

    loginContainer.style.display = 'block';   


    //fetchUserItems(user.userId);

};//end updateUIForLoggedInUser


/////////////Create User Functionality//////////////////

createUserButton.addEventListener('click', async () => {
    const createUsername = document.getElementById('displayName').value;
    const password= document.getElementById('password').value;
    
    if(createUsername){
        try{
            const response = await fetch(`http://localhost:PORTNUMBER/Users/Create${username},${password}`);
    
            const user = await response.json();
    
            updateUIForLoggedInUser(user);

            localStorage.setItem('user', JSON.stringify(user)); //Again, adding that local storage piece in case we want to leverage it
    
        }catch(error){
            console.error('Error Creating Account: ', error);
        }
    }
    });//end createUserClick
    
    function updateUIForLoggedInUser(user) {  //Not sure we need to include this function here again or just call it from above
        
        createUserContainer.style.display = 'none';
       
        welcomeMessage.textContent = `Welcome ${user.userName}!`;
    
        createUserContainer.style.display = 'block';   // What is this doing?
    
    
        //fetchUserItems(user.userId);
    
    };//end updateUIForLoggedInUser

/////////////////////////Logout Handling/////////////

logoutButton.addEventListener('click', () => {
  
    localStorage.removeItem('user');  //Deleting local storage of the 'user' object due to

    loginContainer.style.display = 'block';  //Redisplay the login container

    userContainer.style.display = 'none'; //Need to see if we have to call out each container that has the logout functionality available
});//end of the logoutButton event listener

});//end DOMContentLoaded