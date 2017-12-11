extern "C"
{
    #import <AVFoundation/AVFoundation.h>
    int _add(int x, int y)
    {
        // Just a simple example of returning an int value
        return x + y;
    }
    // Returns a char* (a string to Unity)
    char* _helloWorldString()
    {
        AVCaptureDevice *device = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
        if ([device hasTorch])  
        {
            [device lockForConfiguration:nil];
            //by these you can use Torch Flash Light..
            [device setTorchMode:AVCaptureTorchModeOn];  // use AVCaptureTorchModeOff to turn off
            [device unlockForConfiguration];
        }
        // We can use NSString and go to the c string that Unity wants
        NSString *helloString = @"Hello World";
        // UTF8String method gets us a c string. Then we have to malloc a copy to give to Unity. I reuse a method below that makes it easy.
        return nil;
        /*
        AVCaptureDevice *flashlight = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
        if ([flashlight isTorchAvailable] & [flashlight isTorchModeSupported:AVCaptureTorchModeOn]) {

            BOOL success = [flashlight lockForConfiguration:Nil];
            if(success){
                if(yes==YES){
                    on.hidden = YES;
                    [UIScreen mainScreen].brightness = 1.0;
                    [flashlight setTorchMode:AVCaptureTorchModeOn];
                    [flashlight unlockForConfiguration];
                }
            }
        }
        */
    }

    char* _flashLightOff()
    {
        AVCaptureDevice *device = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
        if ([device hasTorch])  
        {
            [device lockForConfiguration:nil];
            //by these you can use Torch Flash Light..
            [device setTorchMode:AVCaptureTorchModeOff];  // use AVCaptureTorchModeOff to turn off
            [device unlockForConfiguration];
        }
        // We can use NSString and go to the c string that Unity wants
        NSString *helloString = @"Hello World";
        // UTF8String method gets us a c string. Then we have to malloc a copy to give to Unity. I reuse a method below that makes it easy.
        return nil;
        /*
        AVCaptureDevice *flashlight = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
        if ([flashlight isTorchAvailable] & [flashlight isTorchModeSupported:AVCaptureTorchModeOn]) {

            BOOL success = [flashlight lockForConfiguration:Nil];
            if(success){
                if(yes==YES){
                    on.hidden = YES;
                    [UIScreen mainScreen].brightness = 1.0;
                    [flashlight setTorchMode:AVCaptureTorchModeOn];
                    [flashlight unlockForConfiguration];
                }
            }
        }
        */
    }
    //I also like to include these two convenience methods to convert between c string and NSString*. You need to return a copy of the c string so that Unity handles the memory and gets a valid value.
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }
}
