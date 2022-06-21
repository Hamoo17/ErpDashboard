using ErpDashboard.Application.Extensions;
using System.ComponentModel;

namespace ErpDashboard.Application.Enums
{
    public class ErpSystemEnums
    {
        public enum InvoiceType { New, Renew, Migrate };
        public enum InvoiceState { Done, Pending, Canceld };
        public enum Customer_Type { Customer, Sponser }
        public enum Discount_Type { Sponser, General }
        public enum PaymentType { Cash, Visa, CreditCard, ThirdPart, CustomerCredit, Exchange }
        public enum ThirdPartType { Company, Owners, Customer }
        public enum Nfc_Operations { Export, Re_Charg, Re_place, suspended, Redem }

        public enum TaxDicountOption { ApplyBeforDiscount, ApplyAfterDiscount }
        public enum SubscriptionType { Web, mobileApplication, Branch }
        public enum SubscriptionStatus { Active, Expired, Hold, Restricted }
        public enum DeliveryStatus { Pending, Deliveried, NotDelivered, Hold, Resticited, Canceld }
        public enum ActionsTypes { Hold, Active, Resticit, Extend, ChangeName, ChangePhone, ChangeAdress, Changedriver, ChangeBranch, AddNotes, AddDays, AddMeals, changeStartDate, Detact }
        public enum LoggerAction
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }
        public enum ReplacePolicy
        {
            [Description("Regular Policy")][Tips("When Items Unit Are Equal , Will Keep Item Qty")] Regular,
            [Description("Spisific Qty Policy")][Tips("Will Search For Item With Same Qty And Replace It")] SpisificQty,
            [Description("Equational Policy")][Tips("Rplacement With Equation For Each QTY")] Equational,
            [Description("Manual Policy")][Tips("Will Replace an Item And Ask EndUser To Enter The QTY")] Manual
        }

    }
}
