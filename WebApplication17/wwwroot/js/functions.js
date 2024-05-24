document.getElementById("registrationButton").addEventListener("submit", function(event) {
    event.preventDefault(); // Prevent default form submission

    // Gather form data
    const formData = {
        firstName: document.getElementById("firstName").value,
        lastName: document.getElementById("lastName").value,
        regEmail: document.getElementById("regEmail").value,
        regPassword: document.getElementById("regPassword").value,
        confirmPassword: document.getElementById("confirmPassword").value
    };

    // Perform POST request
    fetch('Register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Handle response data if needed
            console.log(data);
            // Assuming success, you might want to display a message or redirect the user
            alert("Registration successful!");
            // Example: window.location.href = '/successPage'; // Redirect to a success page
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            // Handle errors, such as displaying an error message to the user
            alert("An error occurred during registration.");
        });
});