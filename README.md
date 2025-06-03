# Whuhh???
A very simple CLI tool to convert a `.csv` file into `.axaml` ResourceDictionaries, to be used with the [custom localization system](https://github.com/timunie/Tims.Avalonia.Samples/tree/main) by [@timunie](https://github.com/timunie)

# Huhh????
Looks for a `locales.csv` file in the same folder as the compiled `.exe`.  
The `.csv` file should be structured like this:

| key              | en-US         | de-DE         | *other locales...* |
| ---------------- | ------------- | ------------- | ------------------ | 
| greeting.morning | Good morning! | Guten Morgen! | ...                |
| greeting.evening | Good evening! | Guten Abend!  | ...                |

The locale header (`en-US`, `de-DE`) dictate the names of the .axaml files.  
The generated `.axaml` output will look like this:

en-US.axaml
```xaml
<!-- Auto-Generated with DynamicResourceLocaleGenerator.exe -->
<ResourceDictionary xmlns="https://github.com/avaloniaui"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:system="clr-namespace:System;assembly=System.Runtime">
    <system:String x:Key="greeting.morning">Good morning!</system:String>
    <system:String x:Key="greeting.evening">Good evening!</system:String>
</ResourceDictionary>
```

de-DE.axaml
```xaml
<!-- Auto-Generated with DynamicResourceLocaleGenerator.exe -->
<ResourceDictionary xmlns="https://github.com/avaloniaui"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:system="clr-namespace:System;assembly=System.Runtime">
    <system:String x:Key="greeting.morning">Guten Morgen!</system:String>
    <system:String x:Key="greeting.evening">Guten Abend!</system:String>
</ResourceDictionary>
```
