const express = require('express');
const mongoose = require('mongoose');
const app = express();
const ejs = require('ejs');

app.set('view engine', 'ejs');
mongoose.connect('mongodb+srv://mertcansinan:msms3441.@cluster0.kshxa.mongodb.net/cardb?retryWrites=true&w=majority');
app.get('/', (req, res) => {
  res.send('working')
})
app.listen(64300, function () {
  console.log('server is running');

})
   
 