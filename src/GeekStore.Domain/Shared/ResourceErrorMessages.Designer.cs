﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeekStore.Domain.Shared {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GeekStore.Domain.Shared.ResourceErrorMessages", typeof(ResourceErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to City is required..
        /// </summary>
        public static string ADDRESS_CITY {
            get {
                return ResourceManager.GetString("ADDRESS_CITY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to City cannot exceed 50 characters..
        /// </summary>
        public static string ADDRESS_CITY_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_CITY_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Country is required..
        /// </summary>
        public static string ADDRESS_COUNTRY {
            get {
                return ResourceManager.GetString("ADDRESS_COUNTRY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Country cannot exceed 50 characters..
        /// </summary>
        public static string ADDRESS_COUNTRY_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_COUNTRY_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problem while setting user address..
        /// </summary>
        public static string ADDRESS_ERROR {
            get {
                return ResourceManager.GetString("ADDRESS_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neighborhood cannot exceed 100 characters..
        /// </summary>
        public static string ADDRESS_NEIGHBORHOOD_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_NEIGHBORHOOD_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Address is required and cannot be null..
        /// </summary>
        public static string ADDRESS_NULL {
            get {
                return ResourceManager.GetString("ADDRESS_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Postal Code is required..
        /// </summary>
        public static string ADDRESS_POSTAL_CODE {
            get {
                return ResourceManager.GetString("ADDRESS_POSTAL_CODE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Postal Code cannot exceed 20 characters..
        /// </summary>
        public static string ADDRESS_POSTAL_CODE_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_POSTAL_CODE_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to State is required..
        /// </summary>
        public static string ADDRESS_STATE {
            get {
                return ResourceManager.GetString("ADDRESS_STATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to State cannot exceed 50 characters..
        /// </summary>
        public static string ADDRESS_STATE_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_STATE_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Street is required..
        /// </summary>
        public static string ADDRESS_STREET {
            get {
                return ResourceManager.GetString("ADDRESS_STREET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Street cannot exceed 100 characters.
        /// </summary>
        public static string ADDRESS_STREET_MAX {
            get {
                return ResourceManager.GetString("ADDRESS_STREET_MAX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User created with success..
        /// </summary>
        public static string AUTH_CREATE_USER_SUCCESS {
            get {
                return ResourceManager.GetString("AUTH_CREATE_USER_SUCCESS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication error..
        /// </summary>
        public static string AUTH_DEFAULT_ERROR {
            get {
                return ResourceManager.GetString("AUTH_DEFAULT_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given email already exists..
        /// </summary>
        public static string AUTH_EMAIL_ALREADY_EXISTS {
            get {
                return ResourceManager.GetString("AUTH_EMAIL_ALREADY_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email claim not found..
        /// </summary>
        public static string AUTH_EMAIL_CLAIM_NOT_FOUND {
            get {
                return ResourceManager.GetString("AUTH_EMAIL_CLAIM_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while attempt to create user..
        /// </summary>
        public static string AUTH_UNEXPECTED_CREATE_USER_ERROR {
            get {
                return ResourceManager.GetString("AUTH_UNEXPECTED_CREATE_USER_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User not found..
        /// </summary>
        public static string AUTH_USER_NOT_FOUND {
            get {
                return ResourceManager.GetString("AUTH_USER_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User is not signed in..
        /// </summary>
        public static string AUTH_USER_NOT_SIGNED_IN {
            get {
                return ResourceManager.GetString("AUTH_USER_NOT_SIGNED_IN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cart not found..
        /// </summary>
        public static string CART_NOT_FOUND {
            get {
                return ResourceManager.GetString("CART_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error..
        /// </summary>
        public static string DEFAULT_ERROR {
            get {
                return ResourceManager.GetString("DEFAULT_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not found..
        /// </summary>
        public static string DEFAULT_NOT_FOUND {
            get {
                return ResourceManager.GetString("DEFAULT_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Address name is required..
        /// </summary>
        public static string EMPTY_ADDRESS_NAME {
            get {
                return ResourceManager.GetString("EMPTY_ADDRESS_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The house number in the address is required..
        /// </summary>
        public static string EMPTY_ADDRESS_NUMBER {
            get {
                return ResourceManager.GetString("EMPTY_ADDRESS_NUMBER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Customer email is required..
        /// </summary>
        public static string EMPTY_CUSTOMER_EMAIL {
            get {
                return ResourceManager.GetString("EMPTY_CUSTOMER_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delivery Method ID is required..
        /// </summary>
        public static string EMPTY_DELIVERY_METHOD_ID {
            get {
                return ResourceManager.GetString("EMPTY_DELIVERY_METHOD_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Order Id is required..
        /// </summary>
        public static string EMPTY_ORDER_ID {
            get {
                return ResourceManager.GetString("EMPTY_ORDER_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Card brand is required..
        /// </summary>
        public static string EMPTY_PAYMENT_CARD_BRAND {
            get {
                return ResourceManager.GetString("EMPTY_PAYMENT_CARD_BRAND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Card last four digits are required..
        /// </summary>
        public static string EMPTY_PAYMENT_CARD_LAST4 {
            get {
                return ResourceManager.GetString("EMPTY_PAYMENT_CARD_LAST4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment Intent from Stripe was not found..
        /// </summary>
        public static string EMPTY_PAYMENT_INTENT {
            get {
                return ResourceManager.GetString("EMPTY_PAYMENT_INTENT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No payment intent was found for this order..
        /// </summary>
        public static string EMPTY_PAYMENT_INTENT_ID {
            get {
                return ResourceManager.GetString("EMPTY_PAYMENT_INTENT_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Postal Code is required..
        /// </summary>
        public static string EMPTY_POSTAL_CODE {
            get {
                return ResourceManager.GetString("EMPTY_POSTAL_CODE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product brand is required..
        /// </summary>
        public static string EMPTY_PRODUCT_BRAND {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_BRAND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product description is required..
        /// </summary>
        public static string EMPTY_PRODUCT_DESCRIPTION {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product height is required..
        /// </summary>
        public static string EMPTY_PRODUCT_HEIGHT {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_HEIGHT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product Id is required..
        /// </summary>
        public static string EMPTY_PRODUCT_ID {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product length is required..
        /// </summary>
        public static string EMPTY_PRODUCT_LENGTH {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The list of products must contain at least one product, cannot be empty..
        /// </summary>
        public static string EMPTY_PRODUCT_LIST {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_LIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product name is required..
        /// </summary>
        public static string EMPTY_PRODUCT_NAME {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product name is required..
        /// </summary>
        public static string EMPTY_PRODUCT_NAME1 {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_NAME1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product picture is required..
        /// </summary>
        public static string EMPTY_PRODUCT_PICTURE {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_PICTURE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product price is required..
        /// </summary>
        public static string EMPTY_PRODUCT_PRICE {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_PRICE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product quantity is required..
        /// </summary>
        public static string EMPTY_PRODUCT_QUANTITY {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_QUANTITY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product type is required..
        /// </summary>
        public static string EMPTY_PRODUCT_TYPE {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_TYPE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product weight is required..
        /// </summary>
        public static string EMPTY_PRODUCT_WEIGHT {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_WEIGHT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product width is required..
        /// </summary>
        public static string EMPTY_PRODUCT_WIDTH {
            get {
                return ResourceManager.GetString("EMPTY_PRODUCT_WIDTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shopping cart Id is required..
        /// </summary>
        public static string EMPTY_SHOPPING_CART_ID {
            get {
                return ResourceManager.GetString("EMPTY_SHOPPING_CART_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email is required..
        /// </summary>
        public static string EMPTY_USER_EMAIL {
            get {
                return ResourceManager.GetString("EMPTY_USER_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The first name is required..
        /// </summary>
        public static string EMPTY_USER_FIRST_NAME {
            get {
                return ResourceManager.GetString("EMPTY_USER_FIRST_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The last name is required..
        /// </summary>
        public static string EMPTY_USER_LAST_NAME {
            get {
                return ResourceManager.GetString("EMPTY_USER_LAST_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password is required..
        /// </summary>
        public static string EMPTY_USER_PASSWORD {
            get {
                return ResourceManager.GetString("EMPTY_USER_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User&apos;s address is null..
        /// </summary>
        public static string ERROR_ADDRESS_NULL {
            get {
                return ResourceManager.GetString("ERROR_ADDRESS_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product back ordered..
        /// </summary>
        public static string ERROR_BACK_ORDERED {
            get {
                return ResourceManager.GetString("ERROR_BACK_ORDERED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something went wrong while creating the product!.
        /// </summary>
        public static string ERROR_CREATE_PRODUCT {
            get {
                return ResourceManager.GetString("ERROR_CREATE_PRODUCT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No delivery method selected..
        /// </summary>
        public static string ERROR_DELIVERY_METHOD_SELECT {
            get {
                return ResourceManager.GetString("ERROR_DELIVERY_METHOD_SELECT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to get delivery methods..
        /// </summary>
        public static string ERROR_DELIVERY_METHODS_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_DELIVERY_METHODS_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email must be in a valid format email@provider.
        /// </summary>
        public static string ERROR_EMAIL_INVALID {
            get {
                return ResourceManager.GetString("ERROR_EMAIL_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User&apos;s email was not found. You must be logged in..
        /// </summary>
        public static string ERROR_EMAIL_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_EMAIL_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No brands were found..
        /// </summary>
        public static string ERROR_EMPTY_BRANDS {
            get {
                return ResourceManager.GetString("ERROR_EMPTY_BRANDS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your shopping cart has no items..
        /// </summary>
        public static string ERROR_EMPTY_CART_ITEMS {
            get {
                return ResourceManager.GetString("ERROR_EMPTY_CART_ITEMS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No products were found..
        /// </summary>
        public static string ERROR_EMPTY_PRODUCTS {
            get {
                return ResourceManager.GetString("ERROR_EMPTY_PRODUCTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No types were found..
        /// </summary>
        public static string ERROR_EMPTY_TYPES {
            get {
                return ResourceManager.GetString("ERROR_EMPTY_TYPES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to get product item from the database..
        /// </summary>
        public static string ERROR_GET_PRODUCT {
            get {
                return ResourceManager.GetString("ERROR_GET_PRODUCT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email must have a valid format..
        /// </summary>
        public static string ERROR_INVALID_EMAIL {
            get {
                return ResourceManager.GetString("ERROR_INVALID_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property Id must be greater than zero..
        /// </summary>
        public static string ERROR_INVALID_ID {
            get {
                return ResourceManager.GetString("ERROR_INVALID_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid email or password..
        /// </summary>
        public static string ERROR_INVALID_LOGIN {
            get {
                return ResourceManager.GetString("ERROR_INVALID_LOGIN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must contain at least 6 characters..
        /// </summary>
        public static string ERROR_INVALID_PASSWORD_LENGTH {
            get {
                return ResourceManager.GetString("ERROR_INVALID_PASSWORD_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must contain at least one lowercase letter..
        /// </summary>
        public static string ERROR_INVALID_PASSWORD_LOWERCASE {
            get {
                return ResourceManager.GetString("ERROR_INVALID_PASSWORD_LOWERCASE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must contain at least one number..
        /// </summary>
        public static string ERROR_INVALID_PASSWORD_NUMBER {
            get {
                return ResourceManager.GetString("ERROR_INVALID_PASSWORD_NUMBER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must contain at least one special character:  &apos; * $ &amp; ( ) # @..
        /// </summary>
        public static string ERROR_INVALID_PASSWORD_SPECIAL {
            get {
                return ResourceManager.GetString("ERROR_INVALID_PASSWORD_SPECIAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must contain at least one uppercase letter..
        /// </summary>
        public static string ERROR_INVALID_PASSWORD_UPPERCASE {
            get {
                return ResourceManager.GetString("ERROR_INVALID_PASSWORD_UPPERCASE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login failed. Please, try again to log in..
        /// </summary>
        public static string ERROR_LOGIN_FAILED {
            get {
                return ResourceManager.GetString("ERROR_LOGIN_FAILED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No orders were found..
        /// </summary>
        public static string ERROR_ORDER_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_ORDER_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error occurred while trying to persist order in database..
        /// </summary>
        public static string ERROR_ORDER_PERSIST {
            get {
                return ResourceManager.GetString("ERROR_ORDER_PERSIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment was not received for this order..
        /// </summary>
        public static string ERROR_ORDER_STATUS {
            get {
                return ResourceManager.GetString("ERROR_ORDER_STATUS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error ocurred while trying to update Payment Intent. Check if payment was already done..
        /// </summary>
        public static string ERROR_PAYMENT_INTENT {
            get {
                return ResourceManager.GetString("ERROR_PAYMENT_INTENT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The payment status was not succeeded yet..
        /// </summary>
        public static string ERROR_PAYMENT_INVALID_STATUS {
            get {
                return ResourceManager.GetString("ERROR_PAYMENT_INVALID_STATUS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Picture must be a valid URI..
        /// </summary>
        public static string ERROR_PICTURE_URL {
            get {
                return ResourceManager.GetString("ERROR_PICTURE_URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to remove product..
        /// </summary>
        public static string ERROR_REMOVE_PRODUCT {
            get {
                return ResourceManager.GetString("ERROR_REMOVE_PRODUCT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to persist product in database..
        /// </summary>
        public static string ERROR_UPDATE_PRODUCT {
            get {
                return ResourceManager.GetString("ERROR_UPDATE_PRODUCT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The first name must be between 2 and 50 characters..
        /// </summary>
        public static string ERROR_USER_FIRST_NAME_LENGTH {
            get {
                return ResourceManager.GetString("ERROR_USER_FIRST_NAME_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The last name must be between 2 and 50 characters..
        /// </summary>
        public static string ERROR_USER_LAST_NAME_LENGTH {
            get {
                return ResourceManager.GetString("ERROR_USER_LAST_NAME_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successful operation..
        /// </summary>
        public static string GENERIC_SUCCESS_OPERATION {
            get {
                return ResourceManager.GetString("GENERIC_SUCCESS_OPERATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Page size must be between 1 and 10..
        /// </summary>
        public static string INTERVAL_PAGE_SIZE {
            get {
                return ResourceManager.GetString("INTERVAL_PAGE_SIZE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Card brand must have between 2 and 50 caracteres..
        /// </summary>
        public static string INVALID_CARD_BRAND {
            get {
                return ResourceManager.GetString("INVALID_CARD_BRAND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The card&apos;s month expiration date must be between 1 and 12..
        /// </summary>
        public static string INVALID_CARD_EXPMONTH {
            get {
                return ResourceManager.GetString("INVALID_CARD_EXPMONTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The card&apos;s expiration year must be the current year or greater..
        /// </summary>
        public static string INVALID_CARD_YEAR {
            get {
                return ResourceManager.GetString("INVALID_CARD_YEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product brand must not exceed 100 characters..
        /// </summary>
        public static string MAX_PRODUCT_BRAND {
            get {
                return ResourceManager.GetString("MAX_PRODUCT_BRAND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product description must not exceed 300 characters..
        /// </summary>
        public static string MAX_PRODUCT_DESCRIPTION {
            get {
                return ResourceManager.GetString("MAX_PRODUCT_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product name must not exceed 100 characters..
        /// </summary>
        public static string MAX_PRODUCT_NAME {
            get {
                return ResourceManager.GetString("MAX_PRODUCT_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product type must not exceed 100 characters..
        /// </summary>
        public static string MAX_PRODUCT_TYPE {
            get {
                return ResourceManager.GetString("MAX_PRODUCT_TYPE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Page index must be greater than zero..
        /// </summary>
        public static string MIN_PAGE_INDEX {
            get {
                return ResourceManager.GetString("MIN_PAGE_INDEX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product height must be greater than zero..
        /// </summary>
        public static string MIN_PRODUCT_HEIGHT {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_HEIGHT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product length must be greater than zero..
        /// </summary>
        public static string MIN_PRODUCT_LENGTH {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Price must be greater than zero..
        /// </summary>
        public static string MIN_PRODUCT_PRICE {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_PRICE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product quantity cannot be zero or negative..
        /// </summary>
        public static string MIN_PRODUCT_QUANTITY {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_QUANTITY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product weight must be greater than zero..
        /// </summary>
        public static string MIN_PRODUCT_WEIGHT {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_WEIGHT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product width must be greater than zero..
        /// </summary>
        public static string MIN_PRODUCT_WIDTH {
            get {
                return ResourceManager.GetString("MIN_PRODUCT_WIDTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment Summary is required and cannot be null..
        /// </summary>
        public static string PAYMENT_SUMMARY_NOT_NULL {
            get {
                return ResourceManager.GetString("PAYMENT_SUMMARY_NOT_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product Id and url mismatch..
        /// </summary>
        public static string PRODUCT_ID_AND_URL_MISMATCH {
            get {
                return ResourceManager.GetString("PRODUCT_ID_AND_URL_MISMATCH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product was not found..
        /// </summary>
        public static string PRODUCT_NOT_FOUND {
            get {
                return ResourceManager.GetString("PRODUCT_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error while removing from database..
        /// </summary>
        public static string REMOVE_ERROR {
            get {
                return ResourceManager.GetString("REMOVE_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing your request..
        /// </summary>
        public static string REQUEST_ERROR {
            get {
                return ResourceManager.GetString("REQUEST_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Page index is required..
        /// </summary>
        public static string REQUIRED_PAGE_INDEX {
            get {
                return ResourceManager.GetString("REQUIRED_PAGE_INDEX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error with shopping cart was happen..
        /// </summary>
        public static string SHOPPING_CART_ERROR {
            get {
                return ResourceManager.GetString("SHOPPING_CART_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given shopping cart cannot be null..
        /// </summary>
        public static string SHOPPING_CART_NULL {
            get {
                return ResourceManager.GetString("SHOPPING_CART_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error occurred..
        /// </summary>
        public static string UNEXPECTED_ERROR {
            get {
                return ResourceManager.GetString("UNEXPECTED_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Postal Code must be valid with exactly 8 numeric digits..
        /// </summary>
        public static string VALID_POSTAL_CODE {
            get {
                return ResourceManager.GetString("VALID_POSTAL_CODE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Validation Error.
        /// </summary>
        public static string VALIDATION_ERROR_CODE {
            get {
                return ResourceManager.GetString("VALIDATION_ERROR_CODE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A validation problem occurred..
        /// </summary>
        public static string VALIDATION_ERROR_MESSAGE {
            get {
                return ResourceManager.GetString("VALIDATION_ERROR_MESSAGE", resourceCulture);
            }
        }
    }
}
