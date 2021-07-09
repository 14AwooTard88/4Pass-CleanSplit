# Project Abandoned

I will no longer be working on this project mostly out of guilt. There is 1 million post per day so roughly 500k per day split between blue and orange board. Google's reCaptcha cost $1000 per 1M post so they proably end up paying google $500 / day just for captcha or $185k/year just for captcha. One less google engineer mean one more 4chan engineer.

# 4chan Captcha Bypass (Clean)
## Related
This repo only contains Cleaning of the captcha.

For Captcha OCR refer to:
https://github.com/14AwooTard88/4Pass-OCR

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

