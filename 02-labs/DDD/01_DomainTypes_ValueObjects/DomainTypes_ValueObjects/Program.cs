using DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding.ValueObjects;

var y = from x in NonZeroInt5.Create(3)
        select 2025 / x;

int YY = 302;

////Do_Case6_Input_NonZeroInt_Equality();
////Do_Case7_Input_NonZeroInt_SRP();

//void Do_Case6_Input_NonZeroInt_Equality()
//{
//    var b1 = from x in NonZeroInt.Create(6)
//             select x == 6;

//    var b2 = from x in NonZeroInt.Create(6)
//             select x != 6;

//    var b3 = from x in NonZeroInt.Create(6)
//             select 6 == x;

//    var b4 = from x in NonZeroInt.Create(6)
//             select 6 != x;

//    Console.WriteLine($"{b1}, {b2}, {b3}, {b4}");
//}

////void Do_Case7_Input_NonZeroInt_SRP()
////{
////    var result = HandsOnLabs.Case7_Input_NonZeroInt_SRP.NonZeroInt.Create(0);

////    result.IfFail(error =>
////    {
////        Console.WriteLine($"{error}");
////    });
////}