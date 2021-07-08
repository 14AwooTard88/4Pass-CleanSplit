# 4chan Captcha Bypass (Clean)
## Related
This repo only contains Cleaning of the captcha.

For Captcha OCR refer to:
https://github.com/14AwooTard88/4Pass-CleanSplit

For Scraping Captcha and posting refer to:
https://github.com/14AwooTard88/4Pass-Scrape


## Repo Summary
This repo will will the captcha into is slightly more readable version for the OCR to read

![](https://i.imgur.com/uWxqNYH.png)


## Instructions (Cmd)
1. Inside the 'Build' folder, it contains '4Pass-Clean.exe'
2. Run Cmd and type:
```
cd "C:\Path\To\Build\Folder"
4Pass-Clean "C:\Path\To\Captcha.png" "C:\Path\To\Captcha_Clean.png"
```

## Instructions (.cs)
1. Inside the source folder contains 'CleanCaptcha.cs'. You can drag and drop it into your project
Its only 100 lines of code and you need to have 'System.Drawing.Common' nuget package

```
CleanCaptcha.Start(string input_path, string output_path); // input and out should be image path
CleanCaptcha.Start(Bitmap input, out Bitmap output); // input and output should be a bitmap
```

