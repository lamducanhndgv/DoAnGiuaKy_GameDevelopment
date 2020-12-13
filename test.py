import torch.nn as nn
class Net(nn.Module):
    def __init__(self):
        super(Net, self).__init__()
        self.conv2d_2=nn.Conv2d(in_channels=3,out_channels=32,kernel_size=5,stride=1,padding=0,bias=True)
        self.relu_3=nn.ReLU(inplace=False)
        self.maxpool2d_4=nn.MaxPool2d(kernel_size=2,stride=2,padding=0)
        self.flatten_5=nn.Flatten(start_dim=1,end_dim=-1)
        self.linear_6=nn.Linear(in_features=800,out_features=64,bias=True)
        self.relu_7=nn.ReLU(inplace=False)
        self.linear_8=nn.Linear(in_features=64,out_features=10,bias=True)

    def forward(self, x):
        x = self.conv2d_2(x)
        x = self.relu_3(x)
        x = self.maxpool2d_4(x)
        x = self.flatten_5(x)
        x = self.linear_6(x)
        x = self.relu_7(x)
        x = self.linear_8(x)
        return x

