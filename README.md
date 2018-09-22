# CodeCraft.EnumExtension V1.0.0
 
this package allow to retrieve any attributes apply on enum values.


----
## Usages
 An enum __ETestEnum__ on which __Description__
 __attributes__ and Custom attributes (__MyDescriptionAttribute__) have been tagged on enum values.
 
    public class MyDescriptionAttribute : Attribute
    {
        public int Numerical { get; }
        public string Literal { get; }

        public MyDescriptionAttribute(int numerical, string literal)
        {
            Numerical = numerical;
            Literal = literal;
        }
    }
      
    public enum ETestEnum
    {
        [MyDescription(1, "One")]
        [Description("First enum")]
        First,
        [MyDescription(2, "Two")]
        [Description("Second enum")]
        Second,
        [MyDescription(3, "Three")]
        [Description("Third enum")]
        Third
    }
    
### With enum values

this package contains two extensions to retrieve attributes on a specific enum values

##### Retrieve Description:
 Method:

    string DescriptionAttribute(this Enum source);
use case:

    var description = ETestEnum.First.DescriptionAttribute();

##### Retrieve Custom Attribute:
 Method:

    TResult SpecificAttribute<TResult>(this Enum source)
        where TResult : Attribute

use case:

    var MyCustomAttribute = ETestEnum.First.SpecificAttribute<MyDescriptionAttribute>();


### With enum Types

This package contains four extensions to retrieve attributes on a specific enum Type.

**Descriptions**

Two methods for the most use **DescriptionAttribute**
 
* Retrieve an enumarable of all Descriptions
* Retrieve an enumerable of key value pair that contains enum value and associated description.

Methods:

    IEnumerable<string> GetDescriptions();
    IEnumerable<KeyValuePair<E, string>> GetEnumDescriptionPairs();

Use case :

    var allDescriptions = Enum<ETestEnum>.GetDescriptions();
    var pairs = Enum<ETestEnum>.GetEnumDescriptionPairs();


**Custom attributes**

Two methods for CustomAttributes.
In all method **TResult** template inherits from **Attribute**

* Retrieve an enumarable of all CustomAttributes
* Retrieve an enumerable of key value pair that contains enum value and associated CustomAttributes.

Methods:

    IEnumerable<TResult> GetAttributes<TResult>()
    IEnumerable<KeyValuePair<E, TResult>> GetEnumAttributePairs<TResult>()

Use case :

    var allCustomAttributes= Enum<ETestEnum>.GetAttributes<MyDescriptionAttribute>();
    var pairs = Enum<ETestEnum>.GetEnumAttributePairs<MyDescriptionAttribute>();

**Retrieve all values**

One method allow to retrieve all values of enumerator.

    IEnumerable<ETestEnum> allValues = Enum<ETestEnum>.GetValues();

It's equivalent to 

    Enum.GetValues(typeof(E))

 
