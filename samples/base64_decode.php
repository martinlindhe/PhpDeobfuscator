<?php

$encoded = base64_encode('echo "hello\n";'); //  ZWNobyAiaGVsbG9cbiI7

echo eval(base64_decode('ZWNobyAiaGVsbG9cbiI7'));
