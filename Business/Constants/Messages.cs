using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants {
    public static class Messages {
        public static string ProductAdded = "Product added.";
        public static string ProductUpdated = "Product updated.";
        public static string ProductNameMustBeAtLeastTwoCharactersLong = "Product name must be at least 2 characters long.";
        public static string MaintenanceHour = "The system is in maintenance right now. Please try again after 1 hour.";
        public static string ProductCountOfCategoryExceeded = "There can be at most 10 products in a category.";
        public static string ProductWithSameNameExists = "There is already a product with the given name. Please give a different product name.";
        public static string ThereAreTooManyCategories = "There are more than 15 categories right now. In this case, it is not allowed to add a new product.";
        public static string AuthorizationDenied = "You don't have permission for this operation.";
        public static string AuthenticationDenied = "You need to login for this operation.";
        public static string RegisterSuccessful = "Register successful.";
        public static string UserNotFound = "User not found.";
        public static string IncorrectPassword = "Incorrect password.";
        public static string LoginSuccessful = "Login successful.";
        public static string UserWithEmailAlreadyExists = "There is already an user with the given e-mail address. Please give a different e-mail address.";
        public static string AccessTokenCreated = "Access token created.";
    }
}
