
var str = "       blb           rf ns yf[eq     ";
var str2 = "";
var str3 = "";
var count = 0;
var posMax;
var temp;

for (var i = 0; i < str.length; i++) {

    if ((str[i] !== " ") && (str[i + 1] === " ")) {
        count++;
    }
    if ((str[i] !== " ") && (i + 1 === str.length)) {
        count++;
    }
}

for (var el of str) {

    if (el !== " ") {
        str2 += el;
    }
    else if ((str2[str2.length - 1] !== " ") && (str2.length !== 0)) {
        str2 += ",";
        str2 += " ";
    }

}

for (var i = 0; i < str2.length - 1; i++) {

    str3 += str2[i];
}

console.log(count);
console.log(str3);


<style>
.test1 {
    width: 200px;
    height: 200px;
    border: 1px solid black;
}

.test2 {
    background: red !important;
    width: 100px;
    height: 100px;
}
</style>


    <button onclick="onCkickTest()">BUT1</button>



    <button id="btnTest">BUT</button>


    <input type="text" id="inputTest" />


    <div class="test1 qwe" testAttr="123">
    <div class="test2" onclick="pis()">ggggg</div>
    </div>
    <div class="test1 asd"></div>

    <table>
    <tr>
    <td>Слово</td><td>Перевод</td>
    </tr>
    <tr>
    <td>Cabbage</td><td>Капуста</td>
    </tr>
    <tr>
    <td>Carrot</td><td>Морковь</td>
    </tr>
    <tr>
    <td>Potato</td><td>Картофель</td>
    </tr>
    <tr>
    <td>Tomato</td><td>Помидор</td>
    </tr>
    </table>
