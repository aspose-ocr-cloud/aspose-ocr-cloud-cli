# Aspose.OCR Cloud Micro App

Extract text from scans and photos quickly and easily with Aspose.OCR Cloud. Requires minimal resources and works efficiently on entry-level devices.

[Product page](https://products.aspose.cloud/ocr/) | [Documentation](https://docs.aspose.cloud/ocr/) | [Demos](https://github.com/aspose-ocr-cloud) | [Blog](https://blog.aspose.cloud/category/ocr/) | [Free support](https://forum.aspose.cloud/c/ocr/12) | [Terms of service](https://about.aspose.cloud/legal/tos/)

This minimalistic (less that 2MB), fast and resource-wise utility offers a convenient way to interact with the Aspose.OCR Cloud API right from a command-line, eliminating the need to write any code. It can be started from the Windows terminal or PowerShell console, providing the versatile integration into batch files or automated workflows.

Aspose.OCR Cloud offers optical character recognition as a service. With it, you can easily add OCR functionality to almost any device or platform, from netbooks and mini PCs to entry-level smartphones. Resource-intensive operations are offloaded to high-performance servers, guaranteeing the fastest possible speed and minimal load.

Download the source code from [our repository](https://github.com/aspose-ocr-cloud/aspose-ocr-cloud-cli/releases/tag/24.01.0) to get acquainted with Aspose.OCR Cloud SDKs and swiftly and effortlessly develop your own applications.

## Installation

1. Download the latest release from the link below or clone the [repository](https://github.com/aspose-ocr-cloud/aspose-ocr-cloud-cli/).
2. Extract the archive (if necessary).
3. Run the utility (**Aspose.OCR.Cloud.CommandLineTool.exe**) from the command prompt or PowerShell (see command-line parameters below).

## Signing up

In order to use Aspose.OCR Cloud service, you must be signed up for Aspose.OCR Cloud:

1. [Sign up](https://docs.aspose.cloud/ocr/sign-up/) for Aspose Cloud API.
2. Go to [**Applications**](https://dashboard.aspose.cloud/applications) page.
3. Click **Create New Application** button.
4. Give the application an easily recognizable name so it can be quickly found in a long list, and provide an optional detailed description.
5. Create the cloud storage by clicking the _plus_ icon and following the required steps. You can also reuse existing storage, if available.   
   Aspose.OCR Cloud uses its own internal storage, so you can provide the bare minimum storage options:

    - Type: **Internal storage**
    - Storage name: _Any name you like_
    - Storage mode: **Retain files for 24 hours**

6. Click **Save** button.
7. Click the newly created application and copy the values from **Client Id** and **Client Secret** fields.

[Check out more information](https://docs.aspose.cloud/ocr/subscription/) about available subscription plans and a free tier limits.

## Usage

Follow the instructions below to get started with **Aspose.OCR Cloud Micro App**.

### First run

At the first run, you **must** configure the credentials (see **Signing up** chapter above) the app will use to access Aspose.OCR Cloud.

```cmd
Aspose.OCR.Cloud.CommandLineTool.exe config -clientid <your Client Id> -secret <your Client Secret>
```

The credential are saved to **AsposeOCRCloudCLIConfig** [user environment variable](https://learn.microsoft.com/en-us/windows/win32/shell/user-environment-variables). To update credentials, run the same command with the new _Client Id_ and _Client Secret_.

### Recognize an image and save result to file

**IMPORTANT:** Make sure to configure access credentials before performing OCR! See **First run** chapter.

```cmd
Aspose.OCR.Cloud.CommandLineTool.exe recognizeimage -f image.jpg -o recognition.pdf -lang Portuguese
```

Command-line parameters:

- **-f {source image path}** (required) - absolute or relative path to the image file. The utility supports JPEG, PNG, single-page TIFF, GIF and BMP images.
- **-o {target file path}** (required) - absolute or relative path to the file in which the recognition results will be saved.  
  **Important:** If the file extension does not match the file type specified in the **-resultType** parameter, the appropriate extension will be appended to the file name.
- **-lang {language code}** (optional) - [recognition language code](https://docs.aspose.cloud/ocr/supported-languages/), for example `Portuguese` or `HWT_eng`.  
  By default, image text is treated as English.
- **-resultType {see values below}** (optional) - format of the recognition result (by default, the results are saved as plain text):
    - _Text_ - save recognition results to plain text file with line breaks. The extension of a file provided in **-o** parameter should be **.txt**. If the results file extension is different, the appropriate extension will be appended after it.
    - _Pdf_ - the source image will be placed into PDF file with a transparent text overlay above it. The text will be selectable, searchable and indexable. The extension of a file provided in **-o** parameter should be **.pdf**. If the results file extension is different, the appropriate extension will be appended after it.
    - _Hocr_ - save recognition results to hOCR file. The extension of a file provided in **-o** parameter should be **.html**. If the results file extension is different, the appropriate extension will be appended after it.
- **-makeBinarization false** (optional) - use this parameter to disable automatic conversion of the image to [black-and-white](https://docs.aspose.cloud/ocr/binarize-image/).  
  By default, the image is always converted to improve recognition accuracy.
- **-makeSkewCorrection** (optional) - set this flag to force [automatic straightening](https://docs.aspose.cloud/ocr/deskew-image/) of rotated images.
  By default, skew angle correction is disabled.
- **-makeUpsampling** (optional) - set this flag to automatically [increase image resolution](https://docs.aspose.cloud/ocr/upsample-image/) and enhance the contrast of text details.
  By default, smart upsampling is disabled.
- **-dsrMode** (optional) - specify [document structure analysis](https://docs.aspose.cloud/ocr/structure-analysis/) algorithm:
    - _DsrPlusDetector_ (default) - the combination of [complex structure analysis](https://docs.aspose.cloud/ocr/structure-analysis/complex/) and [text area analysis](https://docs.aspose.cloud/ocr/structure-analysis/text/).
    - _DsrAndFilter_ - detect [large blocks of text](https://docs.aspose.cloud/ocr/structure-analysis/complex/), such as paragraphs and columns. Optimal for multi-column documents with illustrations.
    - _DsrNoFilter_ - do not analyze document structure for small images to maximize recognition speed; use [complex structure analysis](https://docs.aspose.cloud/ocr/structure-analysis/complex/) for large images only.
    - _TextDetector_ - find [small text blocks](https://docs.aspose.cloud/ocr/structure-analysis/text/) (individual words, phrases, or lines) inside complex images and then position these blocks relative to each other in recognition results. Works best with sparse irregular text and low-quality photos.
    - _PolygonalTextDetector_ - automatically [straighten](https://docs.aspose.cloud/ocr/structure-analysis/curved/) curved or distorted lines and find small text blocks (individual words, phrases, or lines) inside the resulting image.
    - _NoDsrNoFilter_ - Do not analyze document structure.

## System requirements

Since all OCR tasks are executed in the cloud, the utility does not impose any specific hardware requirements.

The executable can run under the following operating systems:

- Microsoft Windows 10 or above.
- Microsoft Windows Server 2016 or above.

It requires [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to be installed. Other than that, it does not require any third-party components.

**IMPORTANT:** The host must have access to the Internet (**api.aspose.cloud** domain) and the application must be allowed through a firewall (_outbound_ rule).
