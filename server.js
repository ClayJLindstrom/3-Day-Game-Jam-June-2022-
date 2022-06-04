/**
 * Connect to Firebase 
 * */ 

function connectFirebase(){

    const firebaseConfig = {
        apiKey: "AIzaSyDwuB6RBhHqSD1iTWsf7ZPUy-lU3e2-dnE",
        authDomain: "gamejam2022-f7bde.firebaseapp.com",
        databaseURL: "https://gamejam2022-f7bde-default-rtdb.firebaseio.com",
        projectId: "gamejam2022-f7bde",
        storageBucket: "gamejam2022-f7bde.appspot.com",
        messagingSenderId: "716052936442",
        appId: "1:716052936442:web:a89cead518da69f507bb43",
        measurementId: "G-X501GBJBZV"
    };

    try{
        const app = firebase.initializeApp(firebaseConfig);
        return firebase.database();
    } catch (err) {
        console.log(`Error connecting to firebase: ${err}`);
    }    
}

const firedb = connectFirebase();