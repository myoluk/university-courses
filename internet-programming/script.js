// Öğrenci ekleme, ortalama bulma, en yüksek ve en düşük başarı notuna sahip öğrenciyi bulma,
// harf notlarının frekans grafiğini oluşturma, verileri kaydetme (local) işlemlerini yapar

var ogrenciler = new Array();
var ogrenciSayisi = 0;

// Kaydedilmiş veri var ise ogrenciler dizinine ata
if (localStorage.getItem("veri") != null) {

    // verileri yerel hafızadan (local storage) al
    var veri = localStorage.getItem("veri").split(',');

    // hafızaya kaydedilmiş öğrencileri ogrenciler dizisine ata
    for (let i = 0; i < veri.length; i += 7) {
        ogrenciler[ogrenciSayisi] = new Array();
        ogrenciler[ogrenciSayisi][0] = veri[i];
        ogrenciler[ogrenciSayisi][1] = veri[i + 1];
        ogrenciler[ogrenciSayisi][2] = veri[i + 2];
        ogrenciler[ogrenciSayisi][3] = veri[i + 3];
        ogrenciler[ogrenciSayisi][4] = veri[i + 4];
        ogrenciler[ogrenciSayisi][5] = veri[i + 5];
        ogrenciler[ogrenciSayisi][6] = veri[i + 6];
        ogrenciSayisi++;
    }
}

function OgrenciEkle() {
    var ogrenci = new Array();
    var numaralar = new Array();

    // öğrenci bilgilerini textbox tan al
    var numara = document.getElementById("ogr-numara").value;

    // öğrenci mevcut ise kaydetme
    for (let i = 0; i < ogrenciler.length; i++) {
        numaralar[i] = ogrenciler[i][0];
    }
    if (numaralar.indexOf(numara) != -1) {
        alert("Kayıtlı öğrenci girildi!");
        return;
    }

    var ad = document.getElementById("ogr-ad").value;
    var soyad = document.getElementById("ogr-soyad").value;
    var vize = document.getElementById("ogr-vize").value;
    var final = document.getElementById("ogr-final").value;
    var basariNotu = (parseInt(vize) * 0.4 + parseInt(final) * 0.6).toFixed(2);
    var harfNotu = HarfNotuHesapla(basariNotu);

    // öğrenciyi dizi olarak tut
    ogrenci = [numara, ad, soyad, vize, final, basariNotu, harfNotu];

    // öğrenciyi diziye ekle
    ogrenciler[ogrenciSayisi] = ogrenci;

    // eklenen öğrencinin bilgilerini yazdır
    document.getElementById("eklenen-ogr-numara").innerHTML = ogrenciler[ogrenciSayisi][0];
    document.getElementById("eklenen-ogr-ad").innerHTML = ogrenciler[ogrenciSayisi][1];
    document.getElementById("eklenen-ogr-soyad").innerHTML = ogrenciler[ogrenciSayisi][2];
    document.getElementById("eklenen-ogr-vize").innerHTML = ogrenciler[ogrenciSayisi][3];
    document.getElementById("eklenen-ogr-final").innerHTML = ogrenciler[ogrenciSayisi][4];
    document.getElementById("eklenen-ogr-basarinotu").innerHTML = ogrenciler[ogrenciSayisi][5];
    document.getElementById("eklenen-ogr-harfnotu").innerHTML = ogrenciler[ogrenciSayisi][6];

    ogrenciSayisi++;

    // eklenen öğrenciyi kaydet
    localStorage.setItem("veri", ogrenciler);

    OgrenciListele();
}

function OgrenciListele() {
    var outputHTML = "";

    // öğrenci kayıtlı değil ise bilgi ver ve çık
    if (ogrenciler.length == 0) {
        outputHTML = "<tr><td colspan='8'>Kayıtlı öğrenci yok.</tr></td>";
        document.getElementById("ogrenci-listesi").innerHTML = outputHTML;
        document.getElementById("en-yuksek-ogr").innerHTML = outputHTML;
        document.getElementById("en-dusuk-ogr").innerHTML = outputHTML;
        outputHTML = "<tr><td colspan='3'>Kayıtlı öğrenci yok.</tr></td>";
        document.getElementById("frekans-grafigi").innerHTML = outputHTML;
        outputHTML = "<tr><td colspan='2' style='text-align: left;'>Kayıtlı öğrenci yok.</tr></td>";
        document.getElementById("ortalama-tablosu").innerHTML = outputHTML;
        return;
    }

    // öğrenci mevcut ise her öğrenciyi tabloya ekle
    for (let i = 0; i < ogrenciler.length; i++) {
        outputHTML += "<tr>";
        outputHTML += "<td>" + (i + 1) + ".</td>";
        outputHTML += "<td>" + ogrenciler[i][0] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][1] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][2] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][3] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][4] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][5] + "</td>";
        outputHTML += "<td>" + ogrenciler[i][6] + "</td>";
        outputHTML += "</tr>";
    }
    document.getElementById("ogrenci-listesi").innerHTML = outputHTML;

    // ortalama tablosunu, 
    // en yüksek başarı notuna sahip öğrenci tablosunu,
    // en düşük başarı notuna sahip öğrenci tablosunu doldur
    OrtalamaYuksekDusuk();

    // harf notlarının frekans grafiğini oluştur
    FrekansGrafik();
}

function OrtalamaYuksekDusuk() {
    // ortalama, en yüksek ve en düşük başarı notu tabloları

    var vizeOrt = 0, finalOrt = 0, basariOrt = 0;
    var enYuksekOgr = 0, enDusukOgr = 0;

    // ortalama, en yüksek ve en düşük hesaplama
    for (let i = 0; i < ogrenciler.length; i++) {
        vizeOrt += parseInt(ogrenciler[i][3]);
        finalOrt += parseInt(ogrenciler[i][4]);
        basariOrt += parseInt(ogrenciler[i][5]);

        if (ogrenciler[i][5] > ogrenciler[enYuksekOgr][5]) {
            enYuksekOgr = i;
        }
        if (ogrenciler[i][5] < ogrenciler[enDusukOgr][5]) {
            enDusukOgr = i;
        }
    }
    vizeOrt = parseFloat(vizeOrt / ogrenciler.length).toFixed(2);
    finalOrt = parseFloat(finalOrt / ogrenciler.length).toFixed(2);
    basariOrt = parseFloat(basariOrt / ogrenciler.length).toFixed(2);

    // ortalama tablosu
    document.getElementById("ortalama-tablosu").innerHTML =
        "<tr><td>Vizelerin Ortalaması:</td><td>" + vizeOrt + "</td>" +
        "<tr><td>Finallerin Ortalaması:</td><td>" + finalOrt + "</td>" +
        "<tr><td>Başarı Notlarının Ortalaması:</td><td>" + basariOrt + "</td>";

    // en yüksek başarı notuna sahip öğrenci tablosu
    document.getElementById("en-yuksek-ogr").innerHTML = "<tr>" +
        "<td>" + (enYuksekOgr + 1) + ".</td>" +
        "<td>" + ogrenciler[enYuksekOgr][0] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][1] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][2] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][3] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][4] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][5] + "</td>" +
        "<td>" + ogrenciler[enYuksekOgr][6] + "</td>" +
        "</tr>";

    // en düşük başarı notuna sahip öğrenci tablosu
    document.getElementById("en-dusuk-ogr").innerHTML = "<tr>" +
        "<td>" + (enDusukOgr + 1) + ".</td>" +
        "<td>" + ogrenciler[enDusukOgr][0] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][1] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][2] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][3] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][4] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][5] + "</td>" +
        "<td>" + ogrenciler[enDusukOgr][6] + "</td>" +
        "</tr>";
}

function FrekansGrafik() {
    // harf notu frekans grafiği için hesaplamalar
    var harfA = harfB = harfC = harfD = 0;
    var yuzdeA = yuzdeB = yuzdeC = yuzdeD = 0;
    var toplamHarf = 0;
    for (let i = 0; i < ogrenciler.length; i++) {
        switch (ogrenciler[i][6]) {
            case 'A':
                harfA++;
                break;
            case 'B':
                harfB++;
                break;
            case 'C':
                harfC++;
                break;
            case 'D':
                harfD++;
                break;
        }
    }
    toplamHarf = harfA + harfB + harfC + harfD;
    yuzdeA = harfA / toplamHarf * 100;
    yuzdeB = harfB / toplamHarf * 100;
    yuzdeC = harfC / toplamHarf * 100;
    yuzdeD = harfD / toplamHarf * 100;

    // harf notu frekans tgrafiği tablosu
    document.getElementById("frekans-grafigi").innerHTML =
        "<tr><td>A</td><td>" + harfA + "</td><td>" + yuzdeA.toFixed(2) + "</td></tr>" +
        "<tr><td>B</td><td>" + harfB + "</td><td>" + yuzdeB.toFixed(2) + "</td></tr>" +
        "<tr><td>C</td><td>" + harfC + "</td><td>" + yuzdeC.toFixed(2) + "</td></tr>" +
        "<tr><td>D</td><td>" + harfD + "</td><td>" + yuzdeD.toFixed(2) + "</td></tr>";
}

function OgrenciOlustur() {
    // rastgele öğrenci bilgileri oluşturur
    var numaralar = new Array();
    var no1, no2 = 155, no3;
    var numara, ad, soyad, vize, final;
    var adlar = ['Mustafa', 'Ali', 'Bekir', 'Ece', 'Sultan', 'Muzaffer', 'Ahmet',
        'Hasan', 'Cansu', 'Leyla', 'Caner', 'Fatih', 'Oğuz', 'Sibel', 'Elmas'];
    var soyadlar = ['Aksoy', 'Ferman', 'Akyol', 'Yolcu', 'Çiftçi', 'Sakin', 'Koç',
        'Gergin', 'Safkan', 'Malum', 'Çalışkan', 'Erkenci', 'Reyhan', 'Akar', 'Mutlu'];

    // tekil (unique) numara oluşturma
    for (let i = 0; i < ogrenciler.length; i++) {
        numaralar[i] = ogrenciler[i][0];
    }
    do {
        var no1 = RandomDeger(16, 18);
        var no3 = RandomDeger(100, 999);
        numara = no1.toString() + no2.toString() + no3.toString();
    } while (numaralar.indexOf(numara) != -1);

    // diğer öğrenci bilgilerini rastgele oluşturma
    ad = adlar[RandomDeger(0, adlar.length - 1)];
    soyad = soyadlar[RandomDeger(0, soyadlar.length - 1)];
    vize = RandomDeger(10, 100);
    final = RandomDeger(10, 100);

    // bilgileri textboxa aktarma
    document.getElementById("ogr-numara").value = numara;
    document.getElementById("ogr-ad").value = ad;
    document.getElementById("ogr-soyad").value = soyad;
    document.getElementById("ogr-vize").value = vize;
    document.getElementById("ogr-final").value = final;
}

function RandomDeger(min, max) {
    // verilen min ve max aralığında rastgele değer oluştur
    let step1 = max - min + 1;
    let step2 = Math.random() * step1;
    return (Math.floor(step2) + min);
}

function HarfNotuHesapla(ort) {
    var harf = 'D';
    if (ort > 75 && ort <= 100) {
        harf = 'A';
    }
    else if (ort > 50 && ort <= 75) {
        harf = 'B';
    }
    else if (ort > 25 && ort <= 50) {
        harf = 'C';
    }
    else if (ort >= 0 && ort <= 25) {
        harf = 'D';
    }
    return harf;
}

function Temizle() {
    // kayıtlı verileri siler
    delete localStorage.veri;

    document.getElementById("ogr-numara").value = "";
    document.getElementById("ogr-ad").value = "";
    document.getElementById("ogr-soyad").value = "";
    document.getElementById("ogr-vize").value = "";
    document.getElementById("ogr-final").value = "";

    // sayfayı yenile
    window.location.reload();
}

