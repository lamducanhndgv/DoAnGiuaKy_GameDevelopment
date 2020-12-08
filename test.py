import torch.nn as nn
class Net(nn.Module):
    def __init__(self):
        super(Net, self).__init__()
        self.conv2d_2=nn.Conv2d(in_channels=3,out_channels=6,kernel_size=5,stride=1,padding=0,bias=True)
        self.relu_3=nn.ReLU(inplace=False)
        self.maxpool2d_4=nn.MaxPool2d(kernel_size=2,stride=2,padding=0)
        self.conv2d_5=nn.Conv2d(in_channels=6,out_channels=16,kernel_size=5,stride=1,padding=0,bias=True)
        self.relu_6=nn.ReLU(inplace=False)
        self.maxpool2d_7=nn.MaxPool2d(kernel_size=2,stride=2,padding=0)
        self.flatten_9=nn.Flatten(start_dim=1,end_dim=-1)
        self.linear_10=nn.Linear(in_features=400,out_features=120,bias=True)
        self.relu_11=nn.ReLU(inplace=False)
        self.linear_12=nn.Linear(in_features=120,out_features=84,bias=True)
        self.relu_13=nn.ReLU(inplace=False)
        self.linear_14=nn.Linear(in_features=84,out_features=10,bias=True)

    def forward(self, x):
        x = self.conv2d_2(x)
        x = self.relu_3(x)
        x = self.maxpool2d_4(x)
        x = self.conv2d_5(x)
        x = self.relu_6(x)
        x = self.maxpool2d_7(x)
        x = self.flatten_9(x)
        x = self.linear_10(x)
        x = self.relu_11(x)
        x = self.linear_12(x)
        x = self.relu_13(x)
        x = self.linear_14(x)
        return x

