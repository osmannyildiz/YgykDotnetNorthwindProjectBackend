using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants {
    public static class Messages {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductUpdated = "Ürün güncellendi.";
        public static string ProductNameMustBeAtLeastTwoCharactersLong = "Ürün ismi en az 2 karakter uzunluğunda olmalıdır.";
        public static string MaintenanceHour = "Sistem şu anda bakımdadır. Lütfen 1 saat sonra tekrar deneyin.";
        public static string ProductCountOfCategoryExceeded = "Bir kategoride en fazla 10 ürün bulunabilir.";
        public static string ProductWithSameNameExists = "Girilen isimde bir ürün halihazırda mevcut. Lütfen farklı bir ürün ismi girin.";
        public static string ThereAreTooManyCategories = "Şu anda 15'ten fazla kategori var. Bu durumda yeni bir ürün eklenmesine izin verilmiyor.";
        public static string AuthorizationDenied = "Bu işlem için yetkiniz yok.";
        public static string AuthenticationDenied = "Bu işlem için giriş yapmanız gerekmektedir.";
        public static string RegisterSuccessful = "Kullanıcı kayıt işlemi başarılı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string WrongPassword = "Şifre hatalı.";
        public static string LoginSuccessful = "Kullanıcı girişi başarılı.";
        public static string UserWithEmailAlreadyExists = "Bu e-posta adresiyle kayıtlı bir kullanıcı halihazırda mevcut. Lütfen farklı bir e-posta adresi girin.";
        public static string AccessTokenCreated = "Erişim jetonu oluşturuldu.";
    }
}
