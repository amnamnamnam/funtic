// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleButton() {
    const [mass, sugarpercent, milkpercent, fatpercent, cocoapercent] = [
        'mass', 'sugarpercent', 'milkpercent', 'fatpercent', 'cocoapercent'
    ].map(x => document.getElementsByName(x)[0]);
    
    const send_btn = document.getElementById('send');
    const commonPercent = parseInt(sugarpercent.value) + parseInt(milkpercent.value) + parseInt(fatpercent.value) + parseInt(cocoapercent.value);
    console.log('before if', commonPercent);
    if (commonPercent === 100) {
        send_btn.disabled = false;
        console.log('button unlock');
    } else {
        send_btn.disabled = true;
        console.log('button lock');
    }
}

function toggleButton2() {
    const recipename = document.getElementById('recipename');
    const cocoa = document.getElementById('cocoa');
    const fat = document.getElementById('fat');
    const fat0 = document.getElementById('fat0');
    const sugar = document.getElementById('sugar');
    const milk = document.getElementById('milk');
    const send_btn = document.getElementById('send_base');
    const commonPercent = parseInt(sugar.value) + parseInt(milk.value) + parseInt(fat.value) + parseInt(cocoa.value);
    console.log('commonPercent =', commonPercent, 'recipename =', recipename.value);


    document.getElementById('fat0').value = Math.round(cocoa.value * 53 / 100);

    if (commonPercent === 100 && recipename.value != '' && cocoa.value != '0') {
        send_btn.disabled = false;
        console.log('button unlock');
    } else {
        send_btn.disabled = true;
        console.log('button lock');
    }
}

/*function printImage(imageBytes) {
    console.log('Функция запустилась');
    const imageBlob = new Blob([imageBytes], { type: "image/png" });
    console.log('Функция ещё работает');
    const imageUrl = URL.createObjectURL(imageBlob);
    const printWindow = window.open(imageUrl, "Print", "height=500,width=500");
    printWindow.print();
    console.log('Функция завершилась');
}*/