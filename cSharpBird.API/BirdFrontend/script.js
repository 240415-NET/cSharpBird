document.addEventListener('DOMContentLoaded', () => {
const loginContainer = document.getElementById('login-container');
const userContainer = document.getElementById('user-container');
const checklistContainer = document.getElementById('checklist-container');
const checlistCreate = document.getElementById('checklist-create');
const checlistView = document.getElementById('checklist-view');


const loginButton = document.getElementById('login-button');
const logoutButton = document.getElementById('login-button');



loginButton.addEventListener('click', async () => {
const username = document.getElementById('username').value;
const password= document.getElementById('password').value;

if(username){
    try{
        const response = await fetch(`http://localhost:PORTNUMBER/Users/Signin${username},${password}`);

        const user = await response.json();

        updateUIForLoggedInUser(user);

    }catch(error){
        console.error('Error logging in:', error);
    }
}
});//end loginclick

function updateUIForLoggedInUser(user) {
    
    loginContainer.style.display = 'none';
   
    welcomeMessage.textContent = `Welcome ${user.userName}!`;

    che.style.display = 'block';


    //fetchUserItems(user.userId);

};//end updateUIForLoggedInUser



});//end DOMContentLoaded