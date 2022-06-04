let app = {};

app.index = {
    footer: function(){
        $(`footer`).html(`&copy; ${(new Date()).getFullYear()} Game Jam -- Parkour`); 
    },
};

$(document).ready(function(){
    app.index.footer();
});