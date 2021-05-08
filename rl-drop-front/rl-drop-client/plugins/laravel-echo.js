import Echo from 'laravel-echo'
import Cookie from 'js-cookie'

const token = Cookie.get('token')

window.io = require('socket.io-client')
window.Echo = new Echo({
  broadcaster: 'socket.io',
  host: 'http://localhost:6001',
  auth: {
    headers: {
      Authorization: 'Bearer ' + token
    }
  }
})
