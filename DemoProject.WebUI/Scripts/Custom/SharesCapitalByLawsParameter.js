$(document).ready(function () 
{
    // Charges In Percentage Validation
    // blur event: when the element loses focus.
    // focusout event: when the element, or any element inside of it, loses focus.
    $("#admission-fees").blur(function ()
    {
        var admissionFees = parseInt($("#admission-fees").val());

        $('#admission-fees').next("div.info").remove();                    

        if(admissionFees != 10)        
        {
            $('#admission-fees').after('<div class="info text-c-blue">As Per Bylaws Clause 11 - A Member Has Paid Admission Fee Of Rs. 10.</div>');            
        }
    });

    // Nominal Member Admission Fees Validation
    $("#nominal-member-admission-fees").blur(function ()
    {
        var nominalMemberAdmissionFees = parseInt($("#nominal-member-admission-fees").val());

        $('#nominal-member-admission-fees').next("div.info").remove();                    

        if(nominalMemberAdmissionFees != 10)        
        {
            $('#nominal-member-admission-fees').after('<div class="info text-c-blue">As Per Bylaws Clause 18 - MCSA 1960 & Rules 1961 Subject To Provision Any Person May Be Enrolled As Nominal Member Upon His Application In The Prescribed Form And On Payment Of Non Refundable Entrance Fee (Presently Rs. 10/-).</div>');            
        }
    });

    // Enable Nomination Approval Validation On Change Event
    $("#enable-approval-for-employee-nomination").on('change', function ()
    {
        debugger;
        $('#enable-employee-nomination-approval-err-div').next("div.info").remove();            

        if ($(this).is(':checked') == false)
        {
            $('#enable-employee-nomination-approval-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 26 - Prior Approval Of The Board Shall Be Necessary If The Person To Be Nominated Is An Employee Of The Society.</div>');            
        }
    })

    // Employee Nomination Approval Validation
    $("#member-nominee-changes-fee").focus(function ()
    {
        $('#enable-employee-nomination-approval-err-div').next("div.info").remove();                    
        
        if($('#enable-approval-for-employee-nomination').is(':checked') == false)        
        {
            $('#enable-employee-nomination-approval-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 26 - Prior Approval Of The Board Shall Be Necessary If The Person To Be Nominated Is An Employee Of The Society.</div>');            
        }
    });

    // Indemnity Bond For Legal Representative Of Member Validation
    $("#membership-application-minimum-shares").focus(function ()
    {
        $('#enable-legal-representative-indemnity-bond-err-div').next("div.info").remove();                    

        if($('#enable-legal-representative-indemnity-bond').is(':checked') == false)        
        {
            $('#enable-legal-representative-indemnity-bond-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 27 - In The Absence Of Nomination, Legal Representative Of The Deceased Member Shall Executing An Appropriate Deed Of Indemnity And On Any Other Conditions As Laid Down By The Board.</div>');            
        }
    });

    // Membership Application Disposal Period Validation
    $("#application-disposal-period").blur(function ()
    {
        var membershipApplicationDisposalPeriod = parseInt($("#application-disposal-period").val());

        $('#application-disposal-period').next("div.info").remove();                    

        if(membershipApplicationDisposalPeriod != 90)        
        {
            $('#application-disposal-period').after('<div class="info text-c-blue">As Per Bylaws Clause 13 - The Application For Membership Of The Society Found Complete In All Respects Shall Be Disposed Of Within A Period Of 90 Days From The Date Of Receipt Of The Application By The Bank.</div>');
        }
    });

    // Maximum Shares Holding Limit Amount Validation
    $("#shares-holding-limit-percentage").blur(function ()
    {
        var sharesHoldingLimit = parseInt($("#shares-holding-limit-percentage").val());

        $('#shares-holding-limit-percentage').next("div.info").remove();                    

        if(sharesHoldingLimit != 0.2)        
        {
            $('#shares-holding-limit-percentage').after('<div class="info text-c-blue">As Per MCSA 1960 Clause 28 - Provided That The State Government May, By Notification In The Official Gazette, Specify In Respect Of Any Class Of Societies A Higher Or Lower Maximum Than 1/5th (One-Fifth) Of The Share Capital.</div>');
        }
    });

    // Maximum Shares Holding Limit Amount Validation
    $("#shares-holding-limit").blur(function ()
    {
        var sharesHoldingLimit = parseInt($("#shares-holding-limit-amount").val());

        $('#shares-holding-limit-amount').next("div.info").remove();                    

        if(sharesHoldingLimit != 200000)        
        {
            $('#shares-holding-limit-amount').after('<div class="info text-c-blue">As Per MCSA 1960 Clause 28 - Provided That The State Government May, By Notification In The Official Gazette, Specify In Respect Of Any Class Of Societies A Higher Or Lower Maximum Than A Higher Or Lower Amount Than 200000 (Two Lakh) Rupees.</div>');
        }
    });

    // Enable Active Members Annual General Meeting On Change Event
    $("#enable-active-members-agm-attendance").on('change', function ()
    {
        debugger
        $('#enable-active-members-agm-attendance-err-div').next("div.info").remove();            

        if ($(this).is(':checked') == false)
        {
            $('#enable-active-members-agm-attendance-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Sub Clause 1 - To Attend At Least, One General Body Meeting Within In The Previous Five Consecutive Years.</div>');
        }
    })

    // Enable AGM Attendance For Active Membership Validation On active-member-shares-holding focus event.
    $("#active-member-shares-holding").focus(function ()
    {
        $('#enable-active-members-agm-attendance-err-div').next("div.info").remove();            
        $('#enable-minimum-shares-holding-err-div').next("div.info").remove();            

        // Annual General Meeting Attendance
        if($('#enable-active-members-agm-attendance').is(':checked') == false)        
        {
            $('#enable-active-members-agm-attendance-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Sub Clause 1 - To Attend At Least, One General Body Meeting Within In The Previous Five Consecutive Years.</div>');            
        }

        // Minimum Shares Holding
        if($('#enable-minimum-shares-holding').is(':checked') == false)        
        {
            $('#enable-minimum-shares-holding-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - To Be Considered An Active Member, A Member Must Hold At Least One Shares.</div>');
        }
    });

    // Enable Minimum Shares Holding On Change Event
    $("#enable-minimum-shares-holding").on('change', function ()
    {
        $('#enable-minimum-shares-holding-err-div').next("div.info").remove(); 
           
        if ($(this).is(':checked') == false)
        {
            $('#enable-minimum-shares-holding-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - To Be Considered An Active Member, A Member Must Hold At Least One Shares.</div>');
        }
    })

    // Enable Minimum Deposit Holding For Active Membership On Change Event
    $("#enable-minimum-deposit-holding").on('change', function ()
    {
        $('#enable-minimum-deposit-holding-err-div').next("div.info").remove(); 

        if ($(this).is(':checked') == false && $("#enable-minimum-loan-holding").is(':checked') == false && $("#enable-minimum-enjoying-services").is(':checked') == false)
        {
            $('#enable-minimum-deposit-holding-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - Member Must Utilize Any One Service Provided By Society (Any One Deposit, Loan, Other Service) As Per Size Of Society.</div>');
        }
        else
        {
            $('#enable-minimum-deposit-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-loan-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-enjoying-services-err-div').next("div.info").remove(); 
        }
    })

    // Enable Minimum Loan Holding For Active Membership On Change Event
    $("#enable-minimum-loan-holding").on('change', function ()
    {
        $('#enable-minimum-loan-holding-err-div').next("div.info").remove(); 

        if ($(this).is(':checked') == false && $("#enable-minimum-deposit-holding").is(':checked') == false && $("#enable-minimum-enjoying-services").is(':checked') == false)
        {
            $('#enable-minimum-loan-holding-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - Member Must Utilize Any One Service Provided By Society (Any One Deposit, Loan, Other Service) As Per Size Of Society.</div>');
        }
        else
        {
            $('#enable-minimum-deposit-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-loan-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-enjoying-services-err-div').next("div.info").remove(); 
        }
    })

    // Enable Minimum Enjoying Services For Active Membership On Change Event
    $("#enable-minimum-enjoying-services").on('change', function ()
    {
        $('#enable-minimum-enjoying-services-err-div').next("div.info").remove(); 

        if ($(this).is(':checked') == false && $("#enable-minimum-deposit-holding").is(':checked') == false && $("#enable-minimum-loan-holding").is(':checked') == false)
        {
            $('#enable-minimum-enjoying-services-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - Member Must Utilize Any One Service Provided By Society (Any One Deposit, Loan, Other Service) As Per Size Of Society.</div>');
        }
        else
        {
            $('#enable-minimum-deposit-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-loan-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-enjoying-services-err-div').next("div.info").remove(); 
        }
    })

    // Enable Service Utilization, Deposits, Loans Validation On active-member-shares-holding focus event.
    $("#fees-for-shares-transfer").focus(function ()
    {
        $('#enable-active-members-agm-attendance').next("div.info").remove(); 
        $('#enable-minimum-shares-holding').next("div.info").remove(); 

        // Annual General Meeting Attendance
        if($('#enable-active-members-agm-attendance').is(':checked') == false)        
        {
            $('#enable-active-members-agm-attendance-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Sub Clause 1 - To Attend At Least, One General Body Meeting Within In The Previous Five Consecutive Years.</div>');            
        }

        // Minimum Shares Holding
        if($('#enable-minimum-shares-holding').is(':checked') == false)        
        {
            $('#enable-minimum-shares-holding').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - To Be Considered An Active Member, A Member Must Hold At Least One Shares.</div>');
        }

        // Enable At Least One Service Utilization, (Deposits, Loans Validation) Provided By Society.
        if ($("#enable-minimum-deposit-holding").is(':checked') == false && $("#enable-minimum-loan-holding").is(':checked') == false && $("#enable-minimum-enjoying-services").is(':checked') == false)
        {
            $('#enable-minimum-enjoying-services-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 15 - Member Must Utilize Any One Service Provided By Society (Any One Deposit, Loan, Other Service) As Per Size Of Society.</div>');
        }
        else
        {
            $('#enable-minimum-deposit-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-loan-holding-err-div').next("div.info").remove(); 
            $('#enable-minimum-enjoying-services-err-div').next("div.info").remove(); 
        }        
    });

    // Aggregate Deposits Validation   ***** Do - Get Aggreagate Deposit Amount By Ajax
    $("#active-member-aggregate-deposits").blur(function ()
    {
        var activeMemberAggregateDeposit = parseInt($("#active-member-aggregate-deposits").val());

        // ****  var requiredAggreagateDeposit = Call Ajax Method

        var requiredAggreagateDeposit = 1000;

        $('#active-member-aggregate-deposits').next("div.info").remove(); 

        if(activeMemberAggregateDeposit < requiredAggreagateDeposit)        
        {
            $('#active-member-aggregate-deposits').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Sub Clause 2 - Depositor Means A Ordinary Member, Who Has Been Holding Aggregate Deposits (In All Types Of Accounts) Not Less Than The Amount Prescribed Above For The Continuous Period Of Not Less Than One Year.</div>');
        }
    });

    // Aggregate Deposits Time Period / Tenure Validation 
    $("#aggregate-deposit-tenure").blur(function ()
    {
        var activeMemberAggregateDepositTenure = parseInt($("#aggregate-deposit-tenure").val());

        $('#aggregate-deposit-tenure').next("div.info").remove(); 

        if(activeMemberAggregateDeposit < 365)        
        {
            $('#aggregate-deposit-tenure').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Subclause 3B - Depositor Means A Ordinary Member, Who Has Been Holding Aggregate Deposits (In All Types Of Accounts) Not Less Than The Amount Prescribed Above For The Continuous Period Of Not Less Than One Year.</div>');
        }
    });

    // Minimum Borrowing Amount Validation 
    $("#active-member-minimum-borrowing").blur(function ()
    {
        var activeMemberMinimumBorrowing = parseInt($("#active-member-minimum-borrowing").val());

        $('#active-member-minimum-borrowing').next("div.info").remove(); 

        if(activeMemberMinimumBorrowing < 5000)        
        {
            $('#active-member-minimum-borrowing').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Subclause 3A - Borrower Means A Ordinary Member, Who Has Been Holding Credits (In All Types Of Accounts) Not Less Than The Amount Prescribed Above 1000 (One Thousand).</div>');
        }
    });

    // Borrowing Time Period / Tenure  Validation 
    $("#active-member-borrowing-tenure").blur(function ()
    {
        var activeMemberBorrowingTenure = parseInt($("#active-member-borrowing-tenure").val());

        $('#active-member-borrowing-tenure').next("div.info").remove(); 

        if(activeMemberBorrowingTenure < 365)        
        {
            $('#active-member-borrowing-tenure').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Subclause 3A - Borrower Means A Ordinary Member, Who Has Been Holding Credits (In All Types Of Accounts) Not Less Than The Amount Prescribed Above For The Continuous Period Of Not Less Than One Year.</div>');
        }
    });

    // Minimum Service Utilization Validation 
    $("#active-member-minimum-service-utilization").blur(function ()
    {
        var activeMemberMinimumServiceUtilization = parseInt($("#active-member-minimum-service-utilization").val());

        $('#active-member-minimum-service-utilization').next("div.info").remove(); 

        if(activeMemberMinimumServiceUtilization < 1000)        
        {
            $('#active-member-minimum-service-utilization').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Subclause 3C - A Ordinary Member, Who Has Been Enjoying Any Services Not Less Than The Amount Prescribed Above 1000 (One Thousand).</div>');
        }
    });

    // Service Utilization Time Period / Tenure  Validation 
    $("#active-member-service-utilization-tenure").blur(function ()
    {
        var activeMemberServiceUtilizationTenure = parseInt($("#active-member-service-utilization-tenure").val());

        $('#active-member-service-utilization-tenure').next("div.info").remove(); 

        if(activeMemberServiceUtilizationTenure < 365)        
        {
            $('#active-member-service-utilization-tenure').after('<div class="info text-c-blue">As Per Bylaws Clause 15 Subclause 3C - A Ordinary Member, Who Has Been Enjoying Any Service Not Less Than The Amount Prescribed Above For The Continuous Period Of Not Less Than One Year.</div>');
        }
    });

    // Minimum Membership Age For Partial Shares Transfer Validation 
    $("#partial-shares-transfer-membership-age").blur(function ()
    {
        var minimumAgeForPartialSharesTransfer = parseInt($("#partial-shares-transfer-membership-age").val());

        $('#partial-shares-transfer-membership-age').next("div.info").remove(); 

        if(minimumAgeForPartialSharesTransfer < 365)        
        {
            $('#partial-shares-transfer-membership-age').after('<div class="info text-c-blue">As Per Bylaws Clause 30 - A Member May Transfer His Share Or Shares (Where There Shall Not Be Any Accumulated Losses) After Holding Them For Not Less Than One Year To Any Other Member Of The Society Duly Approved By The Board.</div>');
        }
    });

    // Minimum Membership Age For Partial Shares Withdrawl Validation 
    $("#partial-shares-withdrawl-membership-age").blur(function ()
    {
        var minimumAgeForPartialSharesTransfer = parseInt($("#partial-shares-withdrawl-membership-age").val());

        $('#partial-shares-withdrawl-membership-age').next("div.info").remove(); 

        if(minimumAgeForPartialSharesTransfer < 365)        
        {
            $('#partial-shares-withdrawl-membership-age').after('<div class="info text-c-blue">As Per Bylaws Clause 30 - A Member May Withdrawl His Share Or Shares (Where There Shall Not Be Any Accumulated Losses) After Holding Them For Not Less Than One Year To Any Other Member Of The Society Duly Approved By The Board.</div>');
        }
    });

    // Minimum Membership Age For Partial Shares Withdrawl Validation 
    $("#shares-withdrawal-notice-period").blur(function ()
    {
        var sharesWithdrawalNoticePeriod = parseInt($("#shares-withdrawal-notice-period").val());

        $('#shares-withdrawal-notice-period').next("div.info").remove(); 

        if(sharesWithdrawalNoticePeriod < 30)        
        {
            $('#shares-withdrawal-notice-period').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 1 - A Member May Withdraw His Shares After Giving At Least One Month Notice In Writing With The Approval Of The Board.</div>');
        }
    });

    // Aggregate Shares Withdrawl Limit Validation 
    $("#aggregate-shares-withdrawal-limit-in-fy").blur(function ()
    {
        debugger
        var aggregateSharesWithdrawlLimit = parseInt($("#aggregate-shares-withdrawal-limit-in-fy").val());

        $('#aggregate-shares-withdrawal-limit-in-fy').next("div.info").remove(); 

        if (aggregateSharesWithdrawlLimit != 10)        
        {
            $('#aggregate-shares-withdrawal-limit-in-fy').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 1 - A Member May Withdraw His Shares After Giving At Least One Month Notice In Writing With The Approval Of The Board.</div>');
        }
    });

    // Age For Resigning Of Membership Validation 
    $("#membership-age-for-resign-membership").blur(function ()
    {
        debugger;
        var ageForResigningOfMembership = parseInt($("#membership-age-for-resign-membership").val());

        $('#membership-age-for-resign-membership').next("div.info").remove();

        if (ageForResigningOfMembership < 18)        
        {
            $('#membership-age-for-resign-membership').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 1 - A Member May Withdraw Or Resign His Membership After 365 Days.</div>');
        }
    });

    // Membership Waiting Period Before AGM Validation 
//    $("#membership-waiting-period-before-agm").blur(function ()
//    {
//        var membershipWaitingPeriodBeforeAGM = parseInt($("#membership-waiting-period-before-agm").val());

//        if(sharesWithdrawalNoticePeriod != 30)        
//        {
//            $('membership-waiting-period-before-agm').after('<div class="info text-c-blue">As Per Bylaws Clause XX - A Member May Withdraw Or Resign His Membership After One Year.</div>');
//        }
//    }


    // Membership Waiting Period After Resignation Validation 
    $("#membership-waiting-period-after-resignation").blur(function ()
    {
        var membershipWaitingPeriodAfterResignation = parseInt($("#membership-waiting-period-after-resignation").val());

        $('#membership-waiting-period-after-resignation').next("div.info").remove(); 

        if(membershipWaitingPeriodAfterResignation < 365)        
        {
            $('#membership-waiting-period-after-resignation').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 2 - A Member Who Withdraws His Membership Shall Not Be Allowed To Become A Member Again For A Period Of One Year From The Date Of Withdrawal Of Membership.</div>');
        }
    });

    // Membership Waiting Period After Expulsion Validation 
    $("#membership-waiting-period-after-expulsion").blur(function ()
    {
        var membershipWaitingPeriodAfterExpulsion = parseInt($("#membership-waiting-period-after-expulsion").val());

        $('#membership-waiting-period-after-expulsion').next("div.info").remove(); 

        if(membershipWaitingPeriodAfterExpulsion < 365)        
        {
            $('#membership-waiting-period-after-expulsion').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 2 - No Member Of The Society, Who Has Been Expelled, Shall Be Eligible For Readmission As A Member Of The Society For A Period Of One Year From The Date Of Such Expulsion.</div>');
        }
    });

    // Membership Waiting Period After Disqualification Validation 
    $("#membership-waiting-period-after-disqualification").blur(function ()
    {
        var membershipWaitingPeriodAfterDisqualification = parseInt($("#membership-waiting-period-after-disqualification").val());

        $('#membership-waiting-period-after-disqualification').next("div.info").remove(); 

        if(membershipWaitingPeriodAfterDisqualification < 365)        
        {
            $('#membership-waiting-period-after-disqualification').after('<div class="info text-c-blue">As Per Bylaws Clause 22 Subclause 2 - No Member Of The Society, Who Has Been Disqualify, Shall Be Eligible For Readmission As A Member Of The Society For A Period Of One Year From The Date Of Such Disqualification.</div>');
        }
    });

    // Estate Of Deceased Member Liable Period Validation 
    $("#estate-of-deceased-member-liable-period").blur(function ()
    {
        var estateOfDeceasedMemberLiablePeriod = parseInt($("#estate-of-deceased-member-liable-period").val());

        $('#estate-of-deceased-member-liable-period').next("div.info").remove(); 

        if(membershipWaitingPeriodAfterDisqualification < 730)        
        {
            $('#estate-of-deceased-member-liable-period').after('<div class="info text-c-blue">As Per Bylaws Clause 28 Subclause 1A - In The Case Of A Deceased Member, On The Date Of His Death. Shall Continue For A Period Of 2 Years From Such Date.</div>');
        }
    });

    // Enable Indemnity Bond For Duplicate Share Certificate On Change Event
    $("#enable-indemnity-bond-for-duplicate-share-certificate").on('change', function ()
    {
        $('#enable-indemnity-bond-for-duplicate-share-certificate-err-div').next("div.info").remove();            

        if ($(this).is(':checked') == false)
        {
            $('#enable-indemnity-bond-for-duplicate-share-certificate-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 25 Subclause 2 - It Shall However Be Necessary To Produce Evidence To The Satisfaction Of The Board That The Share Certificate’s Were Worn Out, Defaced, Destroyed Or Lost, Or In Absence Of Such Evidence, On Such Indemnity As The Board May Deem Sufficient.</div>');
        }
    })

    // Enable Indemnity Bond For Duplicate Share Certificate On #shares-non-mortgage-loan-ratio Input Focus
    // No Need To Clear Information Message
    $("#shares-non-mortgage-loan-ratio").focus(function ()
    {
        $('#enable-indemnity-bond-for-duplicate-share-certificate-err-div').next("div.info").remove();            

        if($('#enable-indemnity-bond-for-duplicate-share-certificate').is(':checked') == false)        
        {
            $('#enable-indemnity-bond-for-duplicate-share-certificate-err-div').after('<div class="info text-c-blue">As Per Bylaws Clause 25 Subclause 2 - It Shall However Be Necessary To Produce Evidence To The Satisfaction Of The Board That The Share Certificate’s Were Worn Out, Defaced, Destroyed Or Lost, Or In Absence Of Such Evidence, On Such Indemnity As The Board May Deem Sufficient.</div>');
        }
    });

    // Shares Capital To Non Mortgage Loan Ratio Validation 
    $("#shares-non-mortgage-loan-ratio").blur(function ()
    {
        var sharesNonMortgageLoanRatio = parseInt($("#shares-non-mortgage-loan-ratio").val());

        $('#shares-non-mortgage-loan-ratio').next("div.info").remove();            

        if(sharesNonMortgageLoanRatio < 5)        
        {
            $('#shares-non-mortgage-loan-ratio').after('<div class="info text-c-blue">To Get Full Marks (i.e. 10) In Audit Paper Require 5 Percent SharesNonmortgage Ratio.</div>');
        }
    });

    // Shares Capital To Mortgage Loan Ratio Validation 
    $("#shares-mortgage-loan-ratio").blur(function ()
    {
        debugger;
        var sharesMortgageLoanRatio = $("#shares-mortgage-loan-ratio").val();

        $('#shares-mortgage-loan-ratio').next("div.info").remove();            

        if(sharesMortgageLoanRatio < 2.5)        
        {
            $('#shares-mortgage-loan-ratio').after('<div class="info text-c-blue">To Get Full Marks (i.e. 10) In Audit Paper Require 2.5 Percent SharesNonmortgage Ratio.</div>');
        }
    });
});
