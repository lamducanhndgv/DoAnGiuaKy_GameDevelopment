import torch.nn as nn
class Net(nn.Module):
    def __init__(self):
        super(Net, self).__init__()
        self.linear_2=nn.Linear(in_features=10,out_features=20,bias=True)
        self.linear_3=nn.Linear(in_features=20,out_features=1,bias=False)
        self.dropout_4=nn.Dropout(inplace=False)

    def forward(self, x):
        x = self.linear_2(x)
        x = self.linear_3(x)
        x = self.dropout_4(x)
        return x

