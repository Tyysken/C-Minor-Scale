function getHeaders() {
    return {
        'idesk-auth-method': 'up',
        'idesk-auth-username': localStorage.getItem("user"),
        'idesk-auth-password': localStorage.getItem('password'),
        'Accept': 'application/vnd.idesk-v5+json',
    }
}