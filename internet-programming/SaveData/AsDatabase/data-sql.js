// global değişkenler
var ogrenciler = new Array();
var ogrenciSayisi = 0;
var dosyaAdi = 'data_sql.sql';

// Veri tabanı dosyasından veri okuma
var veriOku = function (event) {
    var input = event.target;
    var reader = new FileReader();
    reader.onload = function () {
        var text = reader.result;

        // verileri veri tabanı dosyasından al
        var veri = text.split("CREATE TABLE Ogrenciler(numara VARCHAR(20),ad VARCHAR(20),soyad VARCHAR(20),vize INT,final INT);\n");
        veri = veri[1].split("INSERT INTO Ogrenciler(numara,ad,soyad,vize,final) VALUES(");
        veri.shift(); // ilk elamnı (boş) sil
        for (let i = 0; i < veri.length; i++){
            veri[i] = veri[i].replace(/[\n\r\')\;]/g,'');
            veri[i] = veri[i].split(',');
        }

        // kaydedilmiş öğrencileri ogrenciler dizisine ata
        ogrenciSayisi = 0;
        for (let i = 0; i < veri.length; i++) {
            ogrenciler[ogrenciSayisi] = new Array();
            ogrenciler[ogrenciSayisi][0] = veri[i][0];
            ogrenciler[ogrenciSayisi][1] = veri[i][1];
            ogrenciler[ogrenciSayisi][2] = veri[i][2];
            ogrenciler[ogrenciSayisi][3] = veri[i][3];
            ogrenciler[ogrenciSayisi][4] = veri[i][4];
            ogrenciSayisi++;
        }
        dosyaAdi = input.files[0].name;
        OgrenciListele();
    };
    reader.readAsText(input.files[0]);
};

// Veriyi veri tabanı dosyası olarak kaydetme
function Kaydet() {
    var veri = localStorage.getItem("veri").split(',');
    var data = '';

    data += "CREATE TABLE Ogrenciler(" +
        "numara VARCHAR(20)," + 
        "ad VARCHAR(20)," + 
        "soyad VARCHAR(20)," + 
        "vize INT," + 
        "final INT" + 
        ");\n";
    
    for (let i = 0; i < veri.length; i += 5) {
        data += 'INSERT INTO Ogrenciler(numara,ad,soyad,vize,final) VALUES(' +
        "'"+veri[i]+"','"+veri[i+1]+"','"+veri[i+2]+"',"+veri[i+3]+","+veri[i+4]+
        ");";
        if ((i + 5) != veri.length) {
            data += '\n';
        }
    }

    var element = document.createElement('a');
    element.setAttribute('href', 'data:sql/plain;charset=utf-8,' + encodeURIComponent(data));
    element.setAttribute('download', dosyaAdi);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
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
    var basariNotu = BasariNotuHesapla(vize, final);
    var harfNotu = HarfNotuHesapla(basariNotu);

    // öğrenciyi dizi olarak tut
    ogrenci = [numara, ad, soyad, vize, final];

    // öğrenciyi diziye ekle
    ogrenciler[ogrenciSayisi] = ogrenci;

    // eklenen öğrencinin bilgilerini yazdır
    document.getElementById("ogr-bilgi-baslik").innerText = "Eklenen Öğrenci";
    document.getElementById("eklenen-ogr-numara").innerHTML = ogrenciler[ogrenciSayisi][0];
    document.getElementById("eklenen-ogr-ad").innerHTML = ogrenciler[ogrenciSayisi][1];
    document.getElementById("eklenen-ogr-soyad").innerHTML = ogrenciler[ogrenciSayisi][2];
    document.getElementById("eklenen-ogr-vize").innerHTML = ogrenciler[ogrenciSayisi][3];
    document.getElementById("eklenen-ogr-final").innerHTML = ogrenciler[ogrenciSayisi][4];
    document.getElementById("eklenen-ogr-basarinotu").innerHTML = BasariNotuHesapla(ogrenciler[ogrenciSayisi][3], ogrenciler[ogrenciSayisi][4]);
    document.getElementById("eklenen-ogr-harfnotu").innerHTML = HarfNotuHesapla( BasariNotuHesapla(ogrenciler[ogrenciSayisi][3], ogrenciler[ogrenciSayisi][4]) );

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
        outputHTML += "<td>" + BasariNotuHesapla(ogrenciler[i][3], ogrenciler[i][4]) + "</td>";
        outputHTML += "<td>" + HarfNotuHesapla( BasariNotuHesapla(ogrenciler[i][3], ogrenciler[i][4]) ) + "</td>";
        outputHTML += "</tr>";
    }
    document.getElementById("ogrenci-listesi").innerHTML = outputHTML;
}

function OgrenciAra() {
    var numaralar = new Array();
    var aranan = document.getElementById("aranan-ogr-numara").value;
    for (let i = 0; i < ogrenciler.length; i++) {
        numaralar[i] = ogrenciler[i][0];
    }
    aranan = numaralar.indexOf(aranan);
    if (aranan != -1) {
        document.getElementById("ogr-bilgi-baslik").innerText = "Bulunan Öğrenci";
        document.getElementById("eklenen-ogr-numara").innerHTML = ogrenciler[aranan][0];
        document.getElementById("eklenen-ogr-ad").innerHTML = ogrenciler[aranan][1];
        document.getElementById("eklenen-ogr-soyad").innerHTML = ogrenciler[aranan][2];
        document.getElementById("eklenen-ogr-vize").innerHTML = ogrenciler[aranan][3];
        document.getElementById("eklenen-ogr-final").innerHTML = ogrenciler[aranan][4];
        document.getElementById("eklenen-ogr-basarinotu").innerHTML = BasariNotuHesapla(ogrenciler[aranan][3], ogrenciler[aranan][4]);
        document.getElementById("eklenen-ogr-harfnotu").innerHTML = HarfNotuHesapla( BasariNotuHesapla(ogrenciler[aranan][3], ogrenciler[aranan][4]) );
    }
    else {
        window.alert("Böyle bir kayıt bulunmamaktadır.");
    }
}

// bu fonksiyon rastgele öğrenci bilgileri oluşturur
// öğrenci eklenirken hız kazanılmış olur
function OgrenciOlustur() {
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

function BasariNotuHesapla(vize, final) {
    return (parseInt(vize) * 0.4 + parseInt(final) * 0.6).toFixed(2);
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
    document.getElementById("aranan-ogr-numara").value = "";

    // sayfayı yenile
    window.location.reload();
}