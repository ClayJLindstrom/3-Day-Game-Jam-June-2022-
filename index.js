let app = {};

app.index = {
    date: new Date(),
    year: new Date().getFullYear(),
    underTen: function(x){return (x < 10 ? x = `0${x}` : x);},
    datestamp: function(){
        return `${app.index.year}${app.index.underTen(app.index.date.getMonth() + 1)}${app.index.underTen(app.index.date.getDate())}`;
    }, 
    footer: function(){
        $(`footer`).html(`&copy; ${app.index.year} Game Jam -- Parkour`); 
    },
    init: function(){
        
    }
};



$(document).ready(function(){
    app.index.footer();
});