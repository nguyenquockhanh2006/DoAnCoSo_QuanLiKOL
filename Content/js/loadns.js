function setInputDate(_id, d, m, y) {
    var _dat = document.querySelector(_id);

    if (d < 10) {
        d = "0" + d;
    };
    if (m < 10) {
        m = "0" + m;
    };

    var data = y + "-" + m + "-" + d;
    console.log(data);
    _dat.value = data;
};